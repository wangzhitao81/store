using Pharmeyes.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FileViewer.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View("LogOn");
        }
        [HttpPost]
        public ActionResult LogOn(string userId, string password)
        {
            try
            {

                FileDataEntities ef = new FileDataEntities();
                var user = ef.SystemUsers.FirstOrDefault(c => c.UserId == userId);

                if (user == null || user.IsDisable == "1" || user.PassWord != FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5"))
                {
                    throw new Exception("登录失败，用户名或密码错误！");
                }

                this.CurrentUser = user;
                if (user.IsAdmin == "1")
                {
                    this.MenuList = ef.SystemMenus.Where(c=>string.IsNullOrEmpty(c.IsDisable) || c.IsDisable == "0").ToList();
                }
                else
                {
//                    string sql = @"select * from SystemMenus
//inner join MenuPowers on UserId='{0}' and SystemMenus.MenuId = MenuPowers.MenuId";
//                    sql = string.Format(sql, userId);
//                    this.MenuList = ef.SystemMenus.SqlQuery(sql).ToList();
                    this.MenuList = user.SystemMenus.Where(c=>string.IsNullOrEmpty(c.IsDisable) || c.IsDisable == "0").ToList();
                }

                //this.ProjectPowerList = ef.ProjectPowers.Where(c => c.UserId == userId).ToList();
                this.ProjectPowerList = user.ProjectPowers.ToList();
                FormsAuthentication.SetAuthCookie(userId, false);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ViewBag.UserName = userId;
                ViewBag.message = e.Message;
            }
            return View();
        }
    }
}