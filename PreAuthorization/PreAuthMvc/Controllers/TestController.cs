using Newtonsoft.Json;
using PagedList;
using PreAuthMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreAuthMvc.Controllers
{
    public class TestController : BaseController
    {
        static TestController()
        {
            list.Add(new RoleViewModel() { RoleCode = "c1", RoleName = "n1" });
            list.Add(new RoleViewModel() { RoleCode = "c2", RoleName = "n2" });
            list.Add(new RoleViewModel() { RoleCode = "c3", RoleName = "n3" });
            list.Add(new RoleViewModel() { RoleCode = "c4", RoleName = "n4" });
            list.Add(new RoleViewModel() { RoleCode = "c5", RoleName = "n5" });
            list.Add(new RoleViewModel() { RoleCode = "c6", RoleName = "n6" });
        }
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        public string Add()
        {
            var r= new
            {
                jsonFlag = true,
                jsonMsg = "Add Success"

            };
            return JsonConvert.SerializeObject(r);
        }
        public string Del()
        {
            list.RemoveAt(0);
            var r=new
            {
                jsonFlag = true,
                jsonMsg = "Del Success"

            };
            return JsonConvert.SerializeObject(r);
        }
        public ActionResult PagedTest1(int pageNo=1)
        {
            int pageSize = 2;
            int totalCount = 0;
            var persons = GetPerson(pageNo, pageSize, ref totalCount);
            var personsAsIPageList = new StaticPagedList<RoleViewModel>(persons, pageNo, pageSize, totalCount);
            ViewData["pageNo"] = pageNo;
            ViewData["totalRecords"] = totalCount;
            ViewData["totalPages"] = totalCount / pageSize;
            //return Json(new { persons = personsAsIPageList, pager = personsAsIPageList.GetMetaData() }, JsonRequestBehavior.AllowGet);
            return View(personsAsIPageList);
        }
        static IList<RoleViewModel> list = new List<RoleViewModel>();
        public IList<RoleViewModel> GetPerson(int pageIndex, int pageSize, ref int totalCount)
        {
            //var persons = (from p in db.Persons
            //               orderby p.PersonID descending
            //               select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            
           
            //totalCount = db.Persons.Count();
            totalCount = list.Count;
            return list;
        }
    }
}