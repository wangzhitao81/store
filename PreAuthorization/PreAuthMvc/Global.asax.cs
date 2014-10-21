using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ralph.UnionPay;

namespace PreAuthMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            PayManager.SERVERDATAPATH = Server.MapPath("./App_Data/");
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisterGlobalFilters(GlobalFilters.Filters);
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new PreAuthMvc.MyAuthorize());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
