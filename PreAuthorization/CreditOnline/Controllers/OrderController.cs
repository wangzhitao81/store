using CRModel.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreditOnline.Controllers
{
    public class OrderController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(OrderController));

        [CRAuthorize]
        public JsonResult CheckUrgent(string nationcode, string reporttype)
        {
            CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient();
            Dictionary<string, bool> result = client.CheckUrgent(HttpContext.User.Identity.Name, nationcode, reporttype);
            return new JsonResult() { JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet, Data = result };
        }

        [CRAuthorize]
        public ActionResult NewOrder(string enterprisename = "")
        {
            ViewBag.LanguageDic = base.DicHelper.LanguageType;
            ViewBag.ReportTypeDic = base.DicHelper.ReportType;
            ViewBag.NationalReportTypeDic = base.DicHelper.NationalReportType;
            ViewBag.ExpressTypeDic = base.DicHelper.ExpressType;
            CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient();
            ViewBag.NationCodeDic = client.GetContractNation(HttpContext.User.Identity.Name);
            return View();
        }

        public JsonResult GetUserContract(string username)
        {
            CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient();
            if (!string.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                var result = client.GetContractDetails(HttpContext.User.Identity.Name);
                return new JsonResult() { Data = result };
            }
            return new JsonResult() { Data = new ContractModel() };
        }

        [CRAuthorize]
        public JsonResult GetDataandFee(string nationcode, string expresstype, string reporttype, string selectedlang)
        {
            CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient();
            var result = client.GetDataandFee(HttpContext.User.Identity.Name.ToString(), nationcode, expresstype, reporttype, selectedlang);
            return new JsonResult() { JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet, Data = result };
        }

        [HttpPost]
        [CRAuthorize]
        public ActionResult NewOrder(OrderModel model)
        {
            if (Request.Params["TranslationLanguage"] == null)
            {
                model.TranslationLanguage = model.HiddenDefaultLanguage;
            }
            else
            {
                model.TranslationLanguage = Request.Params["HiddenSelectedLanguage"].ToString();
            }
            CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient();
            bool result = client.NewOrder(model, client.GetContractDetails(HttpContext.User.Identity.Name));
            return RedirectToAction("NewOrder", "Order");
        }

        [CRAuthorize]
        public ActionResult MultiOrder()
        {
            return View();
        }

        [HttpPost]
        [CRAuthorize]
        public ActionResult ConfirmMultiOrder()
        {
            List<OrderModel> models = (List<OrderModel>)JsonConvert.DeserializeObject<List<OrderModel>>(Request.Params["hiddenData"].ToString());
            CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient();
            client.NewMultiOrder(models, client.GetContractDetails(HttpContext.User.Identity.Name));
            return RedirectToAction("Index", "ReportDetails");
        }

        [HttpPost]
        [CRAuthorize]
        public ActionResult MultiOrder(FormCollection data)
        {
            if (Request.Files != null)
            {
                CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient();
                foreach (string requestFile in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[requestFile];
                    string savelocation = ConfigurationManager.AppSettings["UploadFilePath"];
                    if (!Directory.Exists(savelocation))
                    {
                        Directory.CreateDirectory(savelocation);
                    }

                    var originalFileName = file.FileName;
                    string[] array = originalFileName.Split('.');
                    string namefix = originalFileName.Split('.')[array.Length - 1];
                    string fileName = Guid.NewGuid().ToString() + "." + namefix;
                    string path = Path.Combine(savelocation, fileName);
                    file.SaveAs(path);

                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    OleDbConnection objConn = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Ace.OLEDB.12.0;" + "Data Source=" + path + ";" + "Extended Properties=Excel 12.0;");
                    objConn.Open();
                    DataSet objDataset_CTD = new DataSet();
                    OleDbDataAdapter objAdapter_i = new OleDbDataAdapter();
                    objAdapter_i.TableMappings.Add("Table", "DataTable_Report");
                    OleDbCommand objCmdSelect_i = new OleDbCommand("SELECT * FROM [report$]", objConn);
                    objAdapter_i.SelectCommand = objCmdSelect_i;
                    objAdapter_i.Fill(objDataset_CTD);
                    objConn.Close();

                    List<OrderModel> showModels = new List<OrderModel>();
                    List<OrderModel> failedModels = new List<OrderModel>();
                    try
                    {
                        ContractModel contract = client.GetContractDetails(HttpContext.User.Identity.Name);
                        foreach (DataRow row in objDataset_CTD.Tables["DataTable_Report"].Rows)
                        {
                            List<DicModel> languages = base.DicHelper.LanguageType;
                            List<DicModel> reporttypes = base.DicHelper.ReportType;
                            List<DicModel> expresstypes = base.DicHelper.ExpressType;
                            OrderModel model = new OrderModel();
                            if (contract != null)
                            {
                                model.CustomerProfileNO = contract.CustomerProfileNO;
                                model.ContractNo = contract.ContractNO;
                            }
                            model.CreateUser = HttpContext.User.Identity.Name;
                            model.CustomerReportNO = row["客户报告编号"].ToString();
                            model.RegisterNo = row["注册号"].ToString();
                            model.EnterpriseSuppliedName = row["企业名称"].ToString();
                            model.ENName = row["企业名称"].ToString();
                            model.SuppliedAddress = row["企业地址"].ToString();
                            model.NationCode = "174";
                            model.Remarks = row["订单附加信息"].ToString();
                            model.Contact = row["联系人"].ToString();
                            model.ReportType = "";
                            model.ExpressType = "";
                            var reporttype = reporttypes.FirstOrDefault(m => m.Name == row["报告种类"].ToString());
                            var expresstype = expresstypes.FirstOrDefault(m => m.Name == row["服务类型"].ToString());
                            if (reporttype != null)
                            {
                                model.ReportType = reporttype.Code;
                            }
                            if (expresstype != null)
                            {
                                model.ExpressType = expresstype.Code;
                            }
                            if (!string.IsNullOrEmpty(model.ExpressType) && !string.IsNullOrEmpty(model.ReportType) && !string.IsNullOrEmpty(model.ENName))
                            {
                                if (!string.IsNullOrEmpty(row["翻译语言"].ToString()))
                                {
                                    string[] languagelist = row["翻译语言"].ToString().Split(',');
                                    string langvalue = "";
                                    foreach (string temp in languagelist)
                                    {
                                        langvalue += languages.FirstOrDefault(m => m.Name == temp).Code + ",";
                                    }
                                    model.TranslationLanguage = langvalue.TrimEnd(',');
                                }
                                else
                                {
                                    ContractDetailsModel detailsmodel = contract.ContractDetails.FirstOrDefault(m => m.NationCode == "174");
                                    if (detailsmodel != null)
                                    {
                                        model.TranslationLanguage = detailsmodel.DefaultLanguage;
                                    }
                                }
                                List<string> dataandfee = client.GetDataandFee(contract.CustomerId, model.NationCode, model.ExpressType, model.ReportType, model.TranslationLanguage);
                                if (dataandfee != null && dataandfee.Count == 2)
                                {
                                    model.DeliverDate = dataandfee[0];
                                    model.Fee = dataandfee[1];
                                }
                                Dictionary<string, bool> resu = client.CheckUrgent(HttpContext.User.Identity.Name, "174", model.ReportType);
                                bool support = true;
                                if (model.ExpressType == "Urgent")
                                {
                                    support = resu["Urgent"];
                                }
                                if (model.ExpressType == "Express")
                                {
                                    support = resu["Express"];
                                }
                                if (support)
                                    showModels.Add(model);
                                else
                                {
                                    failedModels.Add(model);
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(model.ExpressType) || !string.IsNullOrEmpty(model.ReportType) || !string.IsNullOrEmpty(model.ENName))
                                {
                                    failedModels.Add(model);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("获取批量订单失败", ex);
                    }
                    ViewData["SubData"] = JsonConvert.SerializeObject(showModels, typeof(List<OrderModel>), new JsonSerializerSettings());
                    ViewData["FailedData"] = JsonConvert.SerializeObject(failedModels, typeof(List<OrderModel>), new JsonSerializerSettings());
                    ViewBag.ReportTypeDic = base.DicHelper.ReportType;
                    ViewBag.ExpressTypeDic = base.DicHelper.ExpressType;
                    return View("~/Views/Order/ConfirmMultiOrder.cshtml", showModels);
                }
            }
            return View();
        }

        [CRAuthorize]
        public ActionResult ReOrder(string creditno, string selectedreporttype)
        {
            CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient();
            EnterpriseInfoModel enterprisemodel = client.GetEnterpriseInfo(creditno, HttpContext.User.Identity.Name);
            ViewBag.NationCodeDic = client.GetContractNation(HttpContext.User.Identity.Name);

            OrderModel model = new OrderModel();
            model.RegisterNo = enterprisemodel.RegisterNO;
            model.ENName = enterprisemodel.ENName;
            model.EnterpriseSuppliedName = enterprisemodel.CNName;
            model.SuppliedAddress = enterprisemodel.CNAddress;
            model.Remarks = enterprisemodel.Remarks;
            model.NationCode = enterprisemodel.NationCode;
            model.NeedInterview = false;

            ViewBag.LanguageDic = base.DicHelper.LanguageType;
            ViewBag.ReportTypeDic = base.DicHelper.ReportType;
            ViewBag.NationalReportTypeDic = base.DicHelper.NationalReportType;
            ViewBag.ExpressTypeDic = base.DicHelper.ExpressType;
            ViewBag.SelectedReportType = selectedreporttype;
            ViewBag.CreditNo = creditno;
            return View(model);
        }

        [HttpPost]
        [CRAuthorize]
        public ActionResult ReOrder(OrderModel model)
        {
            if (Request.Params["TranslationLanguage"] == null)
            {
                model.TranslationLanguage = model.HiddenDefaultLanguage;
            }
            else
            {
                model.TranslationLanguage = Request.Params["HiddenSelectedLanguage"].ToString();
            }
            CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient();
            model.ENName = model.EnterpriseSuppliedName;
            bool result = client.NewOrder(model, client.GetContractDetails(HttpContext.User.Identity.Name));
            return RedirectToAction("Details", "Search", new { Id = Request.Params["creditnoforredirect"].ToString() });
        }

        public string CheckCustomerReportNO(string cusreportno)
        {
            try
            {
                CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient();
                string Exist = client.CheckCustomerReportNO(cusreportno, HttpContext.User.Identity.Name);
                if (Exist == "Exist")
                {
                    return "Exist";
                }
            }
            catch (Exception ex)
            {
                logger.Error("检查是否存在客户报告编号失败.", ex);
            }
            return "";
        }
    }
}
