using CreditOnline.CRServiceProxy;
using CRModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreditOnline.Controllers
{
    public class EmailOrderDetailsController : BaseController
    {
        [CRAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [CRAuthorize]
        public ActionResult CPWPIndex()
        {
            return View();
        }

        [HttpPost]
        [CRAuthorize]
        public JsonResult GetEmailOrderDetails()
        {
            Dictionary<string, string> searchParams = new Dictionary<string, string>();
            searchParams["CurrentUser"] = HttpContext.User.Identity.Name;
            if (Request.Params["SearchOpt"] != null && !string.IsNullOrEmpty(Request.Params["SearchOpt"].ToString()))
            {
                searchParams.Add("SearchOpt", Request.Params["SearchOpt"].ToString());
            }
            else
            {
                searchParams.Add("SearchOpt", "ALL");
            }
            if (Request.Params["FromDate"] != null && !string.IsNullOrEmpty(Request.Params["FromDate"].ToString()))
            {
                searchParams.Add("FromDate", Request.Params["FromDate"].ToString());
            }
            else
            {
                searchParams.Add("FromDate", "ALL");
            }
            if (Request.Params["ToDate"] != null && !string.IsNullOrEmpty(Request.Params["ToDate"].ToString()))
            {
                searchParams.Add("ToDate", Request.Params["ToDate"].ToString());
            }
            else
            {
                searchParams.Add("ToDate", "ALL");
            }
            int rows = Convert.ToInt32(Request.Params["rows"]);
            int currentpage = Convert.ToInt32(Request.Params["page"]);

            int totalcount = 0;
            CRServiceClient client = new CRServiceClient();
            List<EmailErrorDetailsModel> models = client.GetEmailErrorDetais(out totalcount, currentpage, rows, searchParams);
            int totalPages = (int)Math.Ceiling((float)totalcount / (float)rows);
            var jsonData = new
            {
                total = totalPages,
                rows = models,
                totalrecordcount = totalcount
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CRAuthorize]
        public JsonResult GetCPWPSyncDetails()
        {
            Dictionary<string, string> searchParams = new Dictionary<string, string>();
            searchParams["CurrentUser"] = HttpContext.User.Identity.Name;
            if (Request.Params["SearchOpt"] != null && !string.IsNullOrEmpty(Request.Params["SearchOpt"].ToString()))
            {
                searchParams.Add("SearchOpt", Request.Params["SearchOpt"].ToString());
            }
            else
            {
                searchParams.Add("SearchOpt", "ALL");
            }
            if (Request.Params["FromDate"] != null && !string.IsNullOrEmpty(Request.Params["FromDate"].ToString()))
            {
                searchParams.Add("FromDate", Request.Params["FromDate"].ToString());
            }
            else
            {
                searchParams.Add("FromDate", "ALL");
            }
            if (Request.Params["ToDate"] != null && !string.IsNullOrEmpty(Request.Params["ToDate"].ToString()))
            {
                searchParams.Add("ToDate", Request.Params["ToDate"].ToString());
            }
            else
            {
                searchParams.Add("ToDate", "ALL");
            }
            int rows = Convert.ToInt32(Request.Params["rows"]);
            int currentpage = Convert.ToInt32(Request.Params["page"]);

            int totalcount = 0;
            CRServiceClient client = new CRServiceClient();
            List<EmailErrorDetailsModel> models = client.GetSyncCPWPErrorDetais(out totalcount, currentpage, rows, searchParams);
            int totalPages = (int)Math.Ceiling((float)totalcount / (float)rows);
            var jsonData = new
            {
                total = totalPages,
                rows = models,
                totalrecordcount = totalcount
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [CRAuthorize]
        public string Details(string Id)
        {
            CRServiceClient client = new CRServiceClient();
            string emailcontent = string.Empty;
            emailcontent = client.GetEmailDetails(Id);
            return emailcontent;
        }

        [CRAuthorize]
        public string CPWPDetails(string Id)
        {
            CRServiceClient client = new CRServiceClient();
            string emailcontent = string.Empty;
            emailcontent = client.GetCPWPError(Id);
            return emailcontent;
        }
    }
}
