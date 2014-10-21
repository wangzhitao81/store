using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PreAuthMvc.Models;
using PagedList;
using log4net;
using PreAuthMvc.Common;
using Ralph.CommonLib.NHibernate;
using NHibernate;
using PreAuthEntityLib.Vo;
using Ralph.CommonLib.Dao;
using Ralph.CommonLib.Utils;

namespace PreAuthMvc.Controllers
{
    /// <summary>
    /// 角色信息
    /// </summary>
    public class RoleController : BaseController
    {
        private int totalCount = 0;
        
        private static ILog _log = LogManager.GetLogger(ConstValues.LogName);
        static RoleController()
        {
        }
        // GET: Role
        public ActionResult Index(int pageNo = 1)
        {
            SessionHelper sessionHelper = new SessionHelper();
            try
            {
                var dao = new BaseDao<RoleVo, long>(sessionHelper); 
                //var rList = GetData(pageNo, pageSize, ref totalCount);
                var rVoList = dao.LoadPage(pageNo, ConstValues.PageSize, ref totalCount);
                var rList = (from p in rVoList
                             select new RoleViewModel{
                                 Id=p.Id,
                        RoleCode=p.RoleCode,
                        RoleName=p.RoleName
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

        public ActionResult Add()
        {
            return View(new RoleViewModel());
        }
        /// <summary>
        /// 增加页面提交
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitAdd(string formContent)
        {
            SessionHelper sessionHelper = new SessionHelper();
            try
            {
                RoleVo vo;
                var dao = new BaseDao<RoleVo, long>(sessionHelper);
                var recordId = Request.Params["recordId"];
                if (!string.IsNullOrEmpty(recordId)&& !"0".Equals(recordId))
                {
                    vo = dao.LoadById(Convert.ToInt64(recordId));
                }
                else
                {
                    vo = new RoleVo();
                }
                
                vo.RoleCode = Request.Params["RoleCode"];
                vo.RoleName = Request.Params["RoleName"];
                dao.SaveOrUpdateIndTrans(vo);
                ViewData["RedirectAction"] = "Index";
                ViewData["Controller"] = "Role";
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
                var dao = new BaseDao<RoleVo, long>(sessionHelper);
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
                var dao = new BaseDao<RoleVo, long>(sessionHelper);
                var rVo = dao.LoadById(Convert.ToInt64(recordId));
                var rVm = new RoleViewModel()
                {
                    Id = rVo.Id,
                    RoleCode = rVo.RoleCode,
                    RoleName = rVo.RoleName
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