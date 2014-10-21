using CRModel.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CreditOnline.Controllers
{
    public class AccountController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AccountController));
        [HttpPost]
        [ValidateInput(false)]
        public string Login(string username, string password, string entername = "", string searchopt = "0")
        {
            try
            {
                CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient();
                password = MD5Helper.GetMD5Hash(password);
                bool isAuthenticated = client.CheckUser(username, password);
                if (isAuthenticated)
                {
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                1,
                                username,
                                DateTime.Now,
                                DateTime.Now.AddMinutes(30),
                                true,
                                "admin"// user role
                                );
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    System.Web.HttpCookie authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                    string returnurl = Request.UrlReferrer.OriginalString;
                    if (returnurl.EndsWith("/Search"))
                    {
                        returnurl += "/Index?option=" + searchopt + "&input=" + entername;
                    }
                    return returnurl;
                }
            }
            catch (Exception ex)
            {
                logger.Error("用户登陆失败", ex);
            }
            return "failed";
        }

        [CRAuthorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [CRAuthorize]
        public string CheckUserLogin()
        {
            if (string.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                return "failed";
            }
            return "success";
        }

        [CRAuthorize]
        public ActionResult EditPassword()
        {
            return View();
        }

        [HttpPost]
        [CRAuthorize]
        public ActionResult EditPassword(EditPassModel model)
        {
            if (string.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                return RedirectToAction("Index", "Home");
            }
            CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient();
            client.EditPassword(HttpContext.User.Identity.Name, MD5Helper.GetMD5Hash(model.CurrentPass), model.NewPass);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoginView(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult LoginView(LoginModel model, string returnUrl)
        {
            var models = new LoginModel();
            try
            {
                UpdateModel(models);
                CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient();
                models.Password = MD5Helper.GetMD5Hash(models.Password);
                bool isAuthenticated = client.CheckUser(models.UserName, models.Password);
                if (isAuthenticated)
                {
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                1,
                                models.UserName,
                                DateTime.Now,
                                DateTime.Now.AddMinutes(30),
                                true,
                                "admin"// user role
                                );
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    System.Web.HttpCookie authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                }
                else
                {
                    ModelState.AddModelError("", new Exception());
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                logger.Error("用户登陆失败", ex);
            }
            return RedirectToLocal(returnUrl);
        }
    }
}
