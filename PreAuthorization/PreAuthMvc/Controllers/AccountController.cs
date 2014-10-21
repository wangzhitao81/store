using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using log4net;
using Newtonsoft.Json;
using PreAuthBusinessLib.Dao;
using PreAuthBusinessLib.Fo;
using PreAuthEntityLib.Vo;
using PreAuthMvc.Common;
using PreAuthMvc.Models;
using System.Web.Security;
using Ralph.CommonLib.NHibernate;
using Ralph.CommonLib.Utils;
using Ralph.CommonLib.Dao;

namespace PreAuthMvc.Controllers
{
    public class AccountController : BaseController
    {
        private static ILog _log = LogManager.GetLogger(ConstValues.LogName);
        private readonly SessionHelper sessionHelper;

        public AccountController()
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
        [AllowAnonymous]
        public string PortalLogin(string cusparam)
        {
            var loginInfo = JsonConvert.DeserializeObject<LoginViewModel>(cusparam);
            var result = "FALSE";
            try
            {
                var dao = new UserDao(sessionHelper);
                var rl = dao.QueryLogin(new UserFo()
                {
                    AgencyCode = loginInfo.AgencyCode,
                    UserCode = loginInfo.UserCode
                });
                if (rl == null || rl.Count != 1)
                {
                    return result;
                }
                var vo = rl[0];
                if (!vo.Password.Equals(CommonUtil.Convert2Md5(loginInfo.Password)))
                {
                    return result;
                }
                
                //根据角色查询授权功能
                var authorityFo = new AuthorityFo();
                authorityFo.RoleCode = vo.RoleCode;
                var authorityDao = new AuthorityDao(sessionHelper);
                var authorityList = authorityDao.QueryByCondiftion(authorityFo);
                var functionDao = new BaseDao<FunctionVo, Int64>();
                var functionList = functionDao.LoadAll();
                var vmList = (from a in authorityList
                              join f in functionList on a.FunctionCode equals f.FunctionCode
                              select new AuthorityViewModel()
                              {
                                  RoleCode = a.RoleCode,
                                  FunctionCode = f.FunctionCode,
                                  FunctionName = f.FunctionName,
                                  FunctionUrl = f.FunctionUrl
                              }).ToList<AuthorityViewModel>();
                //将该list缓存起来
                if (!GlobalCache.Authorities.Keys.Contains(vo.UserCode))
                {
                    GlobalCache.Authorities.Add(vo.UserCode, vmList);
                }
                else
                {
                    GlobalCache.Authorities[vo.UserCode]= vmList;
                }

                result = "TRUE";
                var authTicket = new FormsAuthenticationTicket(
                    1,
                    vo.UserCode,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    true,
                    "admin" // user role
                    );
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                
                Session["agencyCode"] = loginInfo.AgencyCode;
                return result;
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw;
            }
        }

        public ActionResult logOut()
        {
            if (GlobalCache.Authorities.Keys.Contains(HttpContext.User.Identity.Name))
            {
                GlobalCache.Authorities.Remove(HttpContext.User.Identity.Name);
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("Portal", "Home");
        }
    }
}
