using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PreAuthMvc.Models;
using log4net;
using Ralph.CommonLib.NHibernate;
using PreAuthMvc.Common;
using PreAuthEntityLib.Vo;
using Ralph.CommonLib.Dao;

namespace PreAuthMvc.Controllers
{
    /// <summary>
    /// 功能管理
    /// </summary>
    public class FunctionController : BaseController
    {
        private int totalCount = 0;

        private static ILog _log = LogManager.GetLogger(ConstValues.LogName);
        // GET: Agency
        public ActionResult Index(int pageNo = 1)
        {
            SessionHelper sessionHelper = new SessionHelper();
            try
            {
                var dao = new BaseDao<FunctionVo, long>(sessionHelper);
                var rVoList = dao.LoadPage(pageNo, ConstValues.PageSize, ref totalCount);
                var rList = (from p in rVoList
                             select new FunctionViewModel
                             {
                                 Id = p.Id,
                                 FunctionCode = p.FunctionCode,
                                 FunctionName = p.FunctionName,
                                 FunctionUrl=p.FunctionUrl
                             }).ToList();
                PreAuthUtil.SetPageInfo(ViewData, pageNo, totalCount);
                return View(rList);
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw ex;
            }
            finally
            {
                sessionHelper.CloseSession();
            }

        }
        /// <summary>
        /// 跳转到增加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View(new FunctionViewModel());
        }
        /// <summary>
        /// 增加页面提交
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitAdd()
        {
            SessionHelper sessionHelper = new SessionHelper();
            try
            {
                FunctionVo vo;
                var dao = new BaseDao<FunctionVo, long>(sessionHelper);
                var recordId = Request.Params["recordId"];
                if (!string.IsNullOrEmpty(recordId) && !"0".Equals(recordId))
                {
                    vo = dao.LoadById(Convert.ToInt64(recordId));
                }
                else
                {
                    vo = new FunctionVo();
                }

                vo.FunctionCode = Request.Params["FunctionCode"];
                vo.FunctionName = Request.Params["FunctionName"];
                vo.FunctionUrl = Request.Params["FunctionUrl"];

                dao.SaveOrUpdateIndTrans(vo);
                ViewData["RedirectAction"] = "Index";
                ViewData["Controller"] = "Function";
                return PartialView("~/Views/Shared/CommonSuccess.cshtml");
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw ex;
            }
            finally
            {
                sessionHelper.CloseSession();
            }

        }
        public string Del()
        {
            SessionHelper sessionHelper = new SessionHelper();
            try
            {
                var dao = new BaseDao<FunctionVo, long>(sessionHelper);
                if (!string.IsNullOrEmpty(Request.Params["recordId"]))
                {
                    var recordId = Convert.ToInt64(Request.Params["recordId"]);
                    dao.DeleteByIdIndTrans(recordId);
                }
                var r = new
                {
                    jsonFlag = true,
                    jsonMsg = "Del Success"

                };
                return JsonConvert.SerializeObject(r);
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw ex;
            }
            finally
            {
                sessionHelper.CloseSession();
            }
        }
        public ActionResult Modify()
        {
            SessionHelper sessionHelper = new SessionHelper();
            try
            {
                var recordId = Request.Params["recordId"];
                var dao = new BaseDao<FunctionVo, long>(sessionHelper);
                var rVo = dao.LoadById(Convert.ToInt64(recordId));
                var rVm = new FunctionViewModel()
                {
                    Id = rVo.Id,
                    FunctionCode = rVo.FunctionCode,
                    FunctionName = rVo.FunctionName,
                    FunctionUrl=rVo.FunctionUrl
                };
                return View("Add", rVm);
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw ex;
            }
            finally
            {
                sessionHelper.CloseSession();
            }
        }
    }
}