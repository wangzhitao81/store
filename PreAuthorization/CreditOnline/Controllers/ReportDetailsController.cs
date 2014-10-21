using CreditOnline.CRServiceProxy;
using CRModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreditOnline.Controllers
{
    public class ReportDetailsController : BaseController
    {
        [CRAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [CRAuthorize]
        public JsonResult GetReportDetails()
        {
            Dictionary<string, string> searchParams = new Dictionary<string, string>();

            searchParams["CurrentUser"] = HttpContext.User.Identity.Name;
            if (Request.Params["EnterpriseNameStr"] != null && !string.IsNullOrEmpty(Request.Params["EnterpriseNameStr"].ToString()))
            {
                searchParams.Add("EnterpriseNameStr", Request.Params["EnterpriseNameStr"].ToString());
            }
            else
            {
                searchParams.Add("EnterpriseNameStr", "ALL");
            }
            if (Request.Params["CustomerReportNOStr"] != null && !string.IsNullOrEmpty(Request.Params["CustomerReportNOStr"].ToString()))
            {
                searchParams.Add("CustomerReportNOStr", Request.Params["CustomerReportNOStr"].ToString());
            }
            else
            {
                searchParams.Add("CustomerReportNOStr", "ALL");
            }
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
            List<ReportDetailsModel> models = client.GetReportDetais(out totalcount, currentpage, rows, searchParams);
            int totalPages = (int)Math.Ceiling((float)totalcount / (float)rows);
            var jsonData = new
            {
                total = totalPages,
                rows = models,
                totalrecordcount = totalcount
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}
