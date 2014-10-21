using Pharmeyes.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FileViewer.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (CurrentUser == null)
            {
                return SignOut();
            }
            ViewBag.UserName = CurrentUser.UserName;
            return View();
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Account");
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return PartialView();
        }
        [HttpPost]
        public string ChangePassword(string oldPassword, string newPassword)
        {
            try
            {
               
                if (this.CurrentUser.PassWord != FormsAuthentication.HashPasswordForStoringInConfigFile(oldPassword, "MD5"))
                {
                    return "当前密码错误";
                }
                FileDataEntities ef = new FileDataEntities();
                SystemUser user = ef.SystemUsers.FirstOrDefault(c => c.UserId == this.CurrentUser.UserId);
                user.PassWord = FormsAuthentication.HashPasswordForStoringInConfigFile(newPassword, "MD5");
                ef.SaveChanges();
                this.CurrentUser = user;
            }
            catch (Exception e)
            {
                return e.Message; 
            }


            return "1";
        }
        public JsonResult MenuTree()
        {
            List<object> jsonList = new List<object>();
            foreach (SystemMenu module in this.MenuList.Where(c => string.IsNullOrEmpty(c.ParentId)))
            {
                jsonList.Add(GetJson(module));
            }

            return Json(jsonList);
        }
        private object GetJson(SystemMenu module)
        {

            var childrenModuleList = this.MenuList.Where(c => c.ParentId == module.MenuId);

            if (childrenModuleList.Count() == 0)
            {
                return new { id = module.MenuId,
                             text = module.MenuName,
                             iconCls = module.Icon,
                             attributes = new { url = module.Url}
                };
            }
            else
            {
                List<object> children = new List<object>();
                foreach (SystemMenu childmodule in childrenModuleList)
                {
                    children.Add(GetJson(childmodule));
                }

                return new
                {
                    id = module.MenuId,
                    text = module.MenuName,
                    iconCls = module.Icon,
                    children = children
                };
            }
        }
	}
}