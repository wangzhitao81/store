using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Newtonsoft.Json;
using PreAuthEntityLib.Vo;
using PreAuthMvc.Common;
using PreAuthMvc.Models;
using Ralph.CommonLib.Dao;
using Ralph.CommonLib.NHibernate;
using Ralph.CommonLib.Utils;

namespace PreAuthMvc.Controllers
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public class AuthorityController : BaseController
    {
        private int totalCount = 0;

        private static ILog _log = LogManager.GetLogger(ConstValues.LogName);
        private SessionHelper sessionHelper;

        static AuthorityController()
        {
        }
        // GET: Authority
        public AuthorityController()
        {
            sessionHelper = new SessionHelper();
        }
        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                sessionHelper.CloseSession();
            }
            // 让基类释放自己的资源。基类负责调用GC.SuppressFinalize( )
            base.Dispose(isDisposing);
        }
        // GET: Agency
        public ActionResult Index(int pageNo = 1)
        {
            //SessionHelper sessionHelper = new SessionHelper();
            try
            {
                var dao = new BaseDao<AuthorityVo, long>(sessionHelper);
                var rVoList = dao.LoadPage(pageNo, ConstValues.PageSize, ref totalCount);

                var rList = (from p in rVoList
                             select new AuthorityViewModel
                             {
                                 Id = p.Id,
                                 RoleCode = p.RoleCode,
                                 FunctionCode = p.FunctionCode
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
                //sessionHelper.CloseSession();
            }

        }
        /// <summary>
        /// 跳转到增加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            //SessionHelper sessionHelper = new SessionHelper();
            try
            {
                var util = new PreAuthUtil(this.sessionHelper);
                ViewData["functionList"] = util.GetFunctionSelectListItems();
                ViewData["roleList"] = util.GetRoleSelectListItems();
                return View(new AuthorityViewModel());
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw ex;
            }
            finally
            {
                //sessionHelper.CloseSession();
            }
        }
        /// <summary>
        /// 增加页面提交
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitAdd()
        {
            //SessionHelper sessionHelper = new SessionHelper();
            try
            {
                AuthorityVo vo;
                var dao = new BaseDao<AuthorityVo, long>(sessionHelper);
                var recordId = Request.Params["recordId"];
                if (!string.IsNullOrEmpty(recordId) && !"0".Equals(recordId))
                {
                    vo = dao.LoadById(Convert.ToInt64(recordId));
                }
                else
                {
                    vo = new AuthorityVo();
                }
                
                vo.FunctionCode = Request.Params["FunctionCode"];
                vo.RoleCode = Request.Params["RoleCode"];

                dao.SaveOrUpdateIndTrans(vo);
                ViewData["RedirectAction"] = "Index";
                ViewData["Controller"] = "Authority";
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
                //sessionHelper.CloseSession();
            }

        }
        public string Del()
        {
            //SessionHelper sessionHelper = new SessionHelper();
            try
            {
                var dao = new BaseDao<AuthorityVo, long>(sessionHelper);
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
                //sessionHelper.CloseSession();
            }
        }
        public ActionResult Modify()
        {
            //SessionHelper sessionHelper = new SessionHelper();
            try
            {
                var recordId = Request.Params["recordId"];
                var dao = new BaseDao<AuthorityVo, long>(sessionHelper);
                var rVo = dao.LoadById(Convert.ToInt64(recordId));
                var rVm = new AuthorityViewModel()
                {
                    Id = rVo.Id,
                    FunctionCode = rVo.FunctionCode,
                    RoleCode = rVo.RoleCode
                };
                ViewData["ActionName"] = "修改权限";

                var util = new PreAuthUtil(this.sessionHelper);
                ViewData["functionList"] = util.GetFunctionSelectListItems();
                ViewData["roleList"] = util.GetRoleSelectListItems();
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
                //sessionHelper.CloseSession();
            }
        }
    }
}