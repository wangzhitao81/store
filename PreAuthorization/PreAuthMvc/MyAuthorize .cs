using PreAuthMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PreAuthMvc
{
    public class MyAuthorize:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var pass = false;
            var user=HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(user))
            {
                httpContext.Response.StatusCode = 401;
            }
            else
            {
                pass = true;
            }

            if (pass)
            {
                if ("admin".Equals(user))
                {
                    return pass;
                }
                if (!GlobalCache.Authorities.Keys.Contains(user))
                {
                    return !pass;
                }
                var list = GlobalCache.Authorities[user];
                if (list == null)
                {
                    //httpContext.Response.Redirect("Account/Logout");
                    return !pass;
                }
                foreach (var v in list)
                {
                    if (httpContext.Request.Url.ToString().EndsWith(v.FunctionUrl))
                    {
                        return pass;
                    }
                }
                
                return !pass;
            }
            return pass;
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
                else
                {
                    FormsAuthentication.SignOut();
                }
            }

            base.HandleUnauthorizedRequest(filterContext);
        }
    }
        
    }