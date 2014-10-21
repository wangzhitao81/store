using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreditOnline.Controllers
{
    public class DownloadController : BaseController
    {
        [CRAuthorize]
        public ActionResult DownloadTemplate()
        {
            string path = ConfigurationManager.AppSettings["OrderTemplatePath"];
            string name = ConfigurationManager.AppSettings["OrderTemplateName"];
            return File(path, "application/octet-stream", Url.Encode(name));
        }
    }
}
