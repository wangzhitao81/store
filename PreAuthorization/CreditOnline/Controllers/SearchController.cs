using CreditOnline.CRServiceProxy;
using CRModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreditOnline.Controllers
{
    public class SearchController : BaseController
    {
        public ActionResult Index(string option, string input)
        {
            ViewData["SearchOptions"] = option;
            ViewData["InputSearch"] = input;
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection data)
        {
            ViewData["SearchOptions"] = data["searchOptions"];
            ViewData["InputSearch"] = data["inputSearch"];
            return View();
        }

        public JsonResult GetSearchReportData()
        {
            Dictionary<string, string> searchParams = new Dictionary<string, string>();

            if (Request.Params["InputStr"] != null && !string.IsNullOrEmpty(Request.Params["InputStr"].ToString()))
            {
                searchParams.Add("InputStr", Request.Params["InputStr"].ToString());
            }
            else
            {
                searchParams.Add("InputStr", "ALL");
            }
            if (Request.Params["SearchOpt"] != null && !string.IsNullOrEmpty(Request.Params["SearchOpt"].ToString()))
            {
                searchParams.Add("SearchOpt", Request.Params["SearchOpt"].ToString());
            }
            else
            {
                searchParams.Add("SearchOpt", "ALL");
            }

            int rows = Convert.ToInt32(Request.Params["rows"]);
            int currentpage = Convert.ToInt32(Request.Params["page"]);

            int totalcount = 0;
            CRServiceClient client = new CRServiceClient();
            List<SearchResultModel> models = client.GetSearchReportData(out totalcount, currentpage, rows, searchParams);
            int totalPages = (int)Math.Ceiling((float)totalcount / (float)rows);
            var jsonData = new
            {
                total = totalPages,
                rows = models
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(string Id)
        {
            CRServiceClient client = new CRServiceClient();
            EnterpriseInfoModel model = client.GetEnterpriseInfo(Id, HttpContext.User.Identity.Name);
            Dictionary<DicModel, bool> auth = new Dictionary<DicModel, bool>();
            if (!string.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                ContractModel contractmodel = client.GetContractDetails(HttpContext.User.Identity.Name);
                if (contractmodel != null)
                {
                    foreach (DicModel dicm in base.DicHelper.ReportType)
                    {
                        var temp = contractmodel.ContractDetails.FirstOrDefault(m => m.ReportType == dicm.Code);
                        if (temp != null)
                        {
                            auth.Add(dicm, true);
                        }
                        else
                        {
                            auth.Add(dicm, false);
                        }
                    }
                }
                else
                {
                    foreach (DicModel dicm in base.DicHelper.ReportType)
                    {
                        auth.Add(dicm, false);
                    }
                }
            }
            else
            {
                foreach (DicModel dicm in base.DicHelper.ReportType)
                {
                    auth.Add(dicm, true);
                }
            }
            ViewBag.ReportTypeDicWithAuth = auth;
            ViewBag.LanguageDic = base.DicHelper.LanguageType;
            string langstr = "";
            foreach (DicModel dic in base.DicHelper.LanguageType)
            {
                langstr += dic.Code + ",";
            }
            langstr = langstr.TrimEnd(',');
            ViewBag.LanguageDicString = langstr;
            return View(model);
        }
    }
}
