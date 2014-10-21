using CRModel.Models;
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
            var result = client.GetContractDetails(HttpContext.User.Identity.Name);
            return new JsonResult() { Data = result };
        }


        [HttpPost]
        public ActionResult NewOrder(OrderModel model)
        {
            if (Request.Params["TranslationLanguage"] == null)
            {
                model.TranslationLanguage = model.HiddenDefaultLanguage;
            }
            else
            {
                model.TranslationLanguage = Request.Params["TranslationLanguage"].ToString();
            }
            CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient();
            bool result = client.NewOrder(model);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MultiOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmMultiOrder()
        {
            List<OrderModel> models = (List<OrderModel>)JsonConvert.DeserializeObject<List<OrderModel>>(Request.Params["hiddenData"].ToString());
            CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient();
            foreach (OrderModel m in models)
            {
                client.NewOrder(m);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
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

                    foreach (DataRow row in objDataset_CTD.Tables["DataTable_Report"].Rows)
                    {
                        OrderModel model = new OrderModel();
                        //model.CustomerProfileNO = "";
                        model.CustomerReportNO = row["客户报告编号"].ToString();
                        model.RegisterNo = "";
                        model.EnterpriseSuppliedName = row["提供名称"].ToString();
                        model.ENName = row["提供名称"].ToString();
                        model.SuppliedAddress = row["提供地址"].ToString();
                        model.ReportType = row["报告种类"].ToString();
                        model.ExpressType = row["报告类型"].ToString();
                        model.RegisterNo = row["注册号"].ToString();
                        model.Remarks = row["订单附加信息"].ToString();
                        List<DicModel> languages = base.DicHelper.LanguageType;
                        if (!(string.IsNullOrEmpty(model.ExpressType) || string.IsNullOrEmpty(model.ReportType) || (string.IsNullOrEmpty(model.ENName) && (string.IsNullOrEmpty(model.SuppliedAddress)))))
                        {
                            model.TranslationLanguage = languages.FirstOrDefault(m => m.Name == row["翻译语言"].ToString()).Code;
                            showModels.Add(model);
                        }
                    }
                    ViewData["SubData"] = JsonConvert.SerializeObject(showModels, typeof(List<OrderModel>), new JsonSerializerSettings());
                    return View("~/Views/Order/ConfirmMultiOrder.cshtml", showModels);
                }
            }
            return View();
        }

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
            return View(model);
        }
    }
}
