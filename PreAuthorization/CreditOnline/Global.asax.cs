using CRModel.Models;
using EmailOrder;
using SyncData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace CreditOnline
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //同步邮件下单
            new OrderFromEmail().SyncEmailOrder();

            //同步订单到CPWP
            new SyncToCPWP().SyncOrder();
        }
    }
}