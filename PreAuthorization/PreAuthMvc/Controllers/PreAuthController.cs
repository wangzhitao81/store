using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Newtonsoft.Json;
using NHibernate;
using PreAuthBusinessLib.Bo;
using PreAuthBusinessLib.Dao;
using PreAuthBusinessLib.Fo;
using PreAuthEntityLib.Vo;
using PreAuthMvc.Common;
using PreAuthMvc.Models;
using Ralph.CommonLib.Dao;
using Ralph.CommonLib.NHibernate;

namespace PreAuthMvc.Controllers
{
    public class PreAuthController : BaseController
    {
        private int totalCount = 0;
        private static ILog _log = LogManager.GetLogger(ConstValues.LogName);
        private readonly SessionHelper sessionHelper;
        
        public PreAuthController()
        {
            sessionHelper = new SessionHelper();
        }
        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                sessionHelper.CloseSession();
            }
            // 让基类释放自己的资源。基类负责调用GC.SuppressFinalize()
            base.Dispose(isDisposing);
        }
        //
        // GET: /PreAuth/
        public ActionResult Index()
        {
            try
            {
                var dao = new BaseDao<DictItemVo, long>(sessionHelper);

                var activityTypeList = (from p in dao.LoadAll()
                                        where p.ItemType == "ActivityType"
                                  select new SelectListItem()
                                  {
                                      Value = p.ItemCode,
                                      Text = p.ItemName
                                  }).ToList();
                activityTypeList.Insert(0, new SelectListItem()
                {
                    Selected = true,
                    Value = "0",
                    Text = @"---请选择---"
                });
                ViewData["activityTypeList"] = activityTypeList;

                var activityLevelList = (from p in dao.LoadAll()
                                         where p.ItemType == "ActivityLevel"
                                        select new SelectListItem()
                                        {
                                            Value = p.ItemCode,
                                            Text = p.ItemName
                                        }).ToList();
                activityLevelList.Insert(0, new SelectListItem()
                {
                    Selected = true,
                    Value = "0",
                    Text = @"---请选择---"
                });
                ViewData["activityLevelList"] = activityLevelList;

                var bankCardTypeList = new List<SelectListItem>();
                bankCardTypeList.Add(new SelectListItem() { Text = "贷记卡", Value = "1", Selected = true });
                bankCardTypeList.Add(new SelectListItem() { Text = "借记卡", Value = "2" });
                ViewData["bankCardTypeList"] = bankCardTypeList;

                return View(new PreAuthViewModel()
                {
                    BankCardNo = "5309900599078555",
                    BackCardExpire = "1504",
                    BankCardCvn2 = "214"
                });
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw ex;
            }
        }
        public ActionResult SubmitAdd()
        {
            //SessionHelper helper = new SessionHelper();
            try
            {
                var vo = new PreAuthVo();
                var dao = new BaseDao<PreAuthVo, long>(sessionHelper);

                vo.ActivityTypeCode = Request.Params["ActivityTypeCode"];
                vo.ActivityLevelCode = Request.Params["ActivityLevelCode"];
                vo.TelNumber = Request.Params["TelNumber"];
                vo.CustomerName = Request.Params["CustomerName"];
                vo.CustomerIdNo = Request.Params["CustomerIdNo"];
                vo.BankCardTypeCode = Request.Params["BankCardTypeCode"];
                vo.BankCardNo = Request.Params["BankCardNo"];
                vo.BankCardCvn2 = Request.Params["BankCardCvn2"];
                vo.GuaranteeAmount = Convert.ToDecimal(Request.Params["GuaranteeAmount"]);
                vo.MarketingSerialNum = Request.Params["MarketingSerialNum"];
                vo.CommodityName = Request.Params["CommodityName"];
                vo.BackCardExpire = Request.Params["BackCardExpire"];
                vo.Quantity = Convert.ToInt32(Request.Params["Quantity"]);

                const string dtFormat = "yyyy/MM/dd";

                vo.BuyPhoneDate = DateTime.ParseExact(Request.Params["BuyPhoneDate"], dtFormat, System.Globalization.CultureInfo.CurrentCulture);
                vo.ExpiryDate = DateTime.ParseExact(Request.Params["ExpiryDate"], dtFormat, System.Globalization.CultureInfo.CurrentCulture);
                vo.AgreementDate = DateTime.ParseExact(Request.Params["AgreementDate"], dtFormat, System.Globalization.CultureInfo.CurrentCulture);
                vo.OperTime = DateTime.Now;

                vo.OperUserCode = HttpContext.User.Identity.Name;
                
                vo.StateCode = "1";
                vo.PreAuthResult = "Success"; 
                
                var rnd = new Random();
                vo.OrderNo = DateTime.Now.ToString("yyyyMMddHHmmss") + (rnd.Next(900) + 100).ToString(CultureInfo.CurrentCulture).Trim();
                vo.CustomerIp = Request.UserHostAddress;

                vo.AgencyCode = Session["agencyCode"].ToString();

                var bo = new PreAuthBo(sessionHelper);

                if (bo.SendDoPreAuthRequest(vo))
                {
                    using (var transaction = sessionHelper.Current.BeginTransaction())
                    {
                        try
                        {
                            dao.SaveOrUpdate(vo);
                            transaction.Commit();
                        }
                        catch (Exception nhEx)
                        {
                            _log.Error("NHibernate throw exceptions", nhEx);
                            transaction.Rollback();
                            sessionHelper.ClearSession();
                            throw new Exception("NHibernate throw exceptions", nhEx);
                        }
                    }
                }
                ViewData["RedirectAction"] = "Index";
                ViewData["Controller"] = "PreAuth";
                return PartialView("~/Views/Shared/CommonSuccess.cshtml");

            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw new Exception("PreAuth submit action throw exceptions", ex);
            }
        }

        public ActionResult CompleteIndex()
        {
            return View();
        }
        public ActionResult CompleteQuery(int pageNo = 1)
        {
            //var sessionHelper = new SessionHelper();
            try
            {
                var fo = new PreAuthFo
                {
                    TelNumber = Request.Params["telNumber"],
                    CustomerIdNo = Request.Params["customerIdNo"]
                };

                var dao = new PreAuthDao(sessionHelper);
                var rVoList = dao.QueryByCondition(fo, pageNo, ConstValues.PageSize, ref totalCount);
                var rList = (from p in rVoList
                             select new CompletePreAuthViewModel
                             {
                                 Id = p.Id,
                                 PreAuthInfo = new PreAuthViewModel()
                                 {
                                     TelNumber = p.TelNumber,
                                     CustomerIdNo = p.CustomerIdNo,
                                     CustomerName = p.CustomerName,
                                     StateName = p.StateCode
                                 }
                             }).ToList();

                PreAuthUtil.SetPageInfo(ViewData, pageNo, totalCount);

                return PartialView("~/Views/PreAuth/CompleteQueryResult.cshtml", rList);
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw;
            }
        }
        public string DoComplete()
        {
            var recordId = Request.Params["recordId"];
            var dao = new PreAuthDao(sessionHelper);
            var vo = dao.LoadById(Convert.ToInt64(recordId));
            var bo = new PreAuthBo(sessionHelper);

            if (bo.SendCompleteRequest(vo))
            {
                using (var transaction = sessionHelper.Current.BeginTransaction())
                {
                    try
                    {
                        vo.StateCode = "complete";
                        dao.SaveOrUpdate(vo);
                        transaction.Commit();
                    }
                    catch (Exception nhEx)
                    {
                        _log.Error("NHibernate throw exceptions", nhEx);
                        transaction.Rollback();
                        sessionHelper.ClearSession();
                        throw new Exception("NHibernate throw exceptions", nhEx);
                    }
                }
            }
            var r = new
            {
                jsonFlag = true,
                jsonMsg = "Complete Success"

            };
            return JsonConvert.SerializeObject(r);
            //ViewData["RedirectAction"] = "/CancelPreAuth/Index";
            //return PartialView("~/Views/Shared/CommonSuccess.cshtml");
        }

        public ActionResult CancelCompleteIndex()
        {
            return View();
        }
        public ActionResult CancelCompleteQuery(int pageNo = 1)
        {
            try
            {
                var fo = new PreAuthFo
                {
                    TelNumber = Request.Params["telNumber"],
                    CustomerIdNo = Request.Params["customerIdNo"]
                };

                var dao = new PreAuthDao(sessionHelper);
                var rVoList = dao.QueryByCondition(fo, pageNo, ConstValues.PageSize, ref totalCount);
                var rList = (from p in rVoList
                             select new CompletePreAuthViewModel
                             {
                                 Id = p.Id,
                                 PreAuthInfo = new PreAuthViewModel()
                                 {
                                     TelNumber = p.TelNumber,
                                     CustomerIdNo = p.CustomerIdNo,
                                     CustomerName = p.CustomerName,
                                     StateName = p.StateCode
                                 }
                             }).ToList();

                PreAuthUtil.SetPageInfo(ViewData, pageNo, totalCount);

                return PartialView("~/Views/PreAuth/CancelCompleteQueryResult.cshtml", rList);
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw;
            }
        }
        public string DoCancelComplete()
        {
            var recordId = Request.Params["recordId"];
            var dao = new PreAuthDao(sessionHelper);
            var vo = dao.LoadById(Convert.ToInt64(recordId));
            var bo = new PreAuthBo(sessionHelper);

            if (bo.SendCancelCompleteRequest(vo))
            {
                using (var transaction = sessionHelper.Current.BeginTransaction())
                {
                    try
                    {
                        vo.StateCode = "cancelcomplete";
                        dao.SaveOrUpdate(vo);
                        transaction.Commit();
                    }
                    catch (Exception nhEx)
                    {
                        _log.Error("NHibernate throw exceptions", nhEx);
                        transaction.Rollback();
                        sessionHelper.ClearSession();
                        throw new Exception("NHibernate throw exceptions", nhEx);
                    }
                }
            }
            var r = new
            {
                jsonFlag = true,
                jsonMsg = "Cancel Complete Success"

            };
            return JsonConvert.SerializeObject(r);
        }
    }
}