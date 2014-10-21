using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreAuthMvc.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Portal()
        {
            return View();
        }
	}
}