using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CreditOnline
{
    public class CRAuthorize : System.Web.Mvc.AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool Pass = false;
            if (string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                httpContext.Response.StatusCode = 401;
                Pass = false;
            }
            else
            {
                Pass = true;
            }

            return Pass;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if (HttpContext.Current.User.Identity.Name == null)
                {
                    filterContext.Result = new JsonResult
                    {
                        Data = new { IsSuccess = false, Message = "登录超时,请重新登录" },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    return;
                }
            }

            //if (string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            //{
            //    return;
            //}

            base.HandleUnauthorizedRequest(filterContext);

            //if (filterContext.HttpContext.Response.StatusCode == 401)
            //{
            //    filterContext.Result = new RedirectResult("/");
            //}
        }
    }
}