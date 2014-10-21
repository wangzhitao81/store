using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PreAuthMvc.Models;
using log4net;
using Ralph.CommonLib.NHibernate;
using Ralph.CommonLib.Dao;
using PreAuthMvc.Common;
using PreAuthEntityLib.Vo;
using Ralph.CommonLib.Utils;
using PreAuthBusinessLib.Dao;

namespace PreAuthMvc.Controllers
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserController : BaseController
    {
        private int totalCount = 0;

        private static ILog _log = LogManager.GetLogger(ConstValues.LogName);
        private SessionHelper sessionHelper;
        static UserController()
        {
        }
        public UserController()
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
                var dao = new UserDao(sessionHelper);
                var rVoList = dao.LoadPage(pageNo, ConstValues.PageSize, ref totalCount);

                var rList = (from p in rVoList
                             select new UserViewModel
                             {
                                 Id = p.Id,
                                 UserCode = p.UserCode,
                                 UserName = p.UserName
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
                ViewData["agencyList"] = util.GetAgencySelectListItems();
                ViewData["roleList"] = util.GetRoleSelectListItems();
                return View(new UserViewModel());
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
                UserVo vo;
                var dao = new BaseDao<UserVo, long>(sessionHelper);
                var recordId = Request.Params["recordId"];
                if (!string.IsNullOrEmpty(recordId) && !"0".Equals(recordId))
                {
                    vo = dao.LoadById(Convert.ToInt64(recordId));
                }
                else
                {
                    vo = new UserVo();
                }
                
                vo.UserCode = Request.Params["UserCode"];
                vo.UserName = Request.Params["UserName"];
                vo.Password = CommonUtil.Convert2Md5(Request.Params["Password"]);
                vo.RoleCode = Request.Params["RoleCode"];
                vo.AgencyCode = Request.Params["AgencyCode"];
               
                dao.SaveOrUpdateIndTrans(vo);
                ViewData["RedirectAction"] = "Index";
                ViewData["Controller"] = "User";
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
                var dao = new BaseDao<UserVo, long>(sessionHelper);
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
                var dao = new BaseDao<UserVo, long>(sessionHelper);
                var rVo = dao.LoadById(Convert.ToInt64(recordId));
                var rVm = new UserViewModel()
                {
                    Id = rVo.Id,
                    UserCode = rVo.UserCode,
                    UserName = rVo.UserName,
                    RoleCode = rVo.RoleCode,
                    AgencyCode=rVo.AgencyCode
                };

                var util = new PreAuthUtil(this.sessionHelper);
                ViewData["agencyList"] = util.GetAgencySelectListItems();
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