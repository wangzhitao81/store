using FileViewer.Models;
using Pharmeyes.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace FileViewer.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetUserInfo(int page, int rows, string userName)
        {
            FileDataEntities ef = new FileDataEntities();

            var query = ef.SystemUsers.Select(c => c);
            query = query.Where(c => c.IsAdmin != "1");
            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(c => c.UserName.Contains(userName));
            }
            var userList = query.OrderBy(c => c.Id).Skip((page - 1) * rows).Take(rows).ToList();
            List<UserModel> userModeList = new List<UserModel>();
            foreach (var user in userList)
            {
                UserModel userModel = new UserModel();
                userModel.Id = user.Id;
                userModel.UserId = user.UserId;
                userModel.UserName = user.UserName;
                userModel.IsDisable = user.IsDisable;
                userModeList.Add(userModel);
            }

            return Json(
                new
                {
                    total = query.Count(),
                    rows = userModeList
                }); 

           
        }
        public JsonResult GetConfigList(string userId)
        {

            FileDataEntities ef = new FileDataEntities();
            List<FileUploadConfig> configList = ef.FileUploadConfigs.ToList();
            List<ProjectPower> projectPowerList;
            SystemUser user = ef.SystemUsers.FirstOrDefault(c => c.UserId == userId);
            if (user == null)
            {
                projectPowerList = new List<ProjectPower>();
            }
            else
            {
                projectPowerList =user.ProjectPowers.ToList();
            }


            List<ConfigModel> configModeList = new List<ConfigModel>();
            foreach (var config in configList)
            {
                ConfigModel configModel = new ConfigModel();
                configModel.Id = config.Id;
                configModel.ProjectName = config.ProjectName;
                configModel.AllowRead = "0";
                configModel.AllowDelete = "0";
                var projectPower = projectPowerList.FirstOrDefault(c => c.ProjectName == config.ProjectName);
                if (projectPower != null)
                {
                    configModel.AllowRead = projectPower.AllowRead;
                    configModel.AllowDelete = projectPower.AllowDelete;
                }

                configModeList.Add(configModel);
            }

            return Json(
                new
                {
                    rows = configModeList
                });

        }

        [HttpGet]
        public ActionResult Add()
        {
            //部门
            return PartialView("Form", new UserModel());
        }
        [HttpPost]
        public string Add(UserModel userModel)
        {
        
            try
            {
                List<string> allowRead = new List<string>();
                List<string> allowDelete = new List<string>();
                if (Request.Params["AllowRead"] != null)
                {
                    allowRead.AddRange(Request.Params["AllowRead"].Split(','));
                }
                if (Request.Params["AllowDelete"] != null)
                {
                    allowDelete.AddRange(Request.Params["AllowDelete"].Split(','));
                }

                var userMenu = Request.Params["UserMenu"].Split(',').ToList();
                FileDataEntities ef = new FileDataEntities();
                SystemUser user = new SystemUser();
                user.UserId = userModel.UserId;
                user.UserName = userModel.UserName;
                user.PassWord = FormsAuthentication.HashPasswordForStoringInConfigFile(userModel.PassWord, "MD5");
                user.IsAdmin = "0";
                user.IsDisable = "0";
                if (userModel.IsDisable == "true")
                {
                    user.IsDisable = "1";
                }
                foreach( var menu in ef.SystemMenus.Where(c=>userMenu.Contains(c.MenuId)))
                {
                    user.SystemMenus.Add(menu);
                }
                foreach (var config in ef.FileUploadConfigs.Where(c => allowRead.Contains(c.ProjectName) || allowDelete.Contains(c.ProjectName)))
                {
                    ProjectPower projectPower = projectPower = new ProjectPower();
                    projectPower.UserId = user.UserId;
                    projectPower.ProjectName = config.ProjectName;
                    projectPower.AllowRead = "0";
                    projectPower.AllowDelete = "0";
                    if (allowRead.Contains(config.ProjectName))
                    {
                        projectPower.AllowRead = "1";
                    }
                    if (allowDelete.Contains(config.ProjectName))
                    {
                        projectPower.AllowDelete = "1";
                    }
                    user.ProjectPowers.Add(projectPower);
                    
                }
                ef.SystemUsers.Add(user);
                ef.SaveChanges();

            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "1";
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            FileDataEntities ef = new FileDataEntities();
            UserModel userModel = new UserModel();
            var user = ef.SystemUsers.FirstOrDefault(c => c.Id == id);
            userModel.Id = user.Id;
            userModel.UserId = user.UserId;
            userModel.UserName = user.UserName;
            userModel.IsDisable = user.IsDisable;
            return PartialView("Form", userModel);
        }
        [HttpPost]
        public string Edit(UserModel userModel)
        {
            try
            {
                List<string> allowRead = new List<string>();
                List<string> allowDelete = new List<string>();
                if (Request.Params["AllowRead"] != null)
                {
                    allowRead.AddRange(Request.Params["AllowRead"].Split(','));
                }
                if (Request.Params["AllowDelete"] != null)
                {
                    allowDelete.AddRange(Request.Params["AllowDelete"].Split(','));
                }
                var userMenu = Request.Params["UserMenu"].Split(',').ToList();
                FileDataEntities ef = new FileDataEntities();
                SystemUser user = ef.SystemUsers.FirstOrDefault(c => c.Id == userModel.Id);
                user.UserId = userModel.UserId;
                user.UserName = userModel.UserName;
                user.IsDisable = "0";
                if (userModel.IsDisable == "true")
                {
                    user.IsDisable = "1";
                }
                user.SystemMenus.Clear();
                foreach (var menu in ef.SystemMenus.Where(c => userMenu.Contains(c.MenuId)))
                {
                    user.SystemMenus.Add(menu);
                }
                user.ProjectPowers.Clear();
                foreach (var config in ef.FileUploadConfigs.Where(c => allowRead.Contains(c.ProjectName) || allowDelete.Contains(c.ProjectName)))
                {
                    ProjectPower projectPower = projectPower = new ProjectPower();
                    projectPower.UserId = user.UserId;
                    projectPower.ProjectName = config.ProjectName;
                    projectPower.AllowRead = "0";
                    projectPower.AllowDelete = "0";
                    if (allowRead.Contains(config.ProjectName))
                    {
                        projectPower.AllowRead = "1";
                    }
                    if (allowDelete.Contains(config.ProjectName))
                    {
                        projectPower.AllowDelete = "1";
                    }
                    user.ProjectPowers.Add(projectPower);
                }
                ef.SaveChanges();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "1";
        }

        [HttpPost]
        public string Delete(int id)
        {
            try
            {
                FileDataEntities ef = new FileDataEntities();
                SystemUser user = ef.SystemUsers.FirstOrDefault(c => c.Id == id);
                ef.SystemUsers.Remove(user);
                ef.SaveChanges();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "1";
        }


        [HttpGet]
        public ActionResult ChangePassword(int id)
        {
            ViewBag.Id = id;
            return PartialView();
        }
        [HttpPost]
        public string ChangePassword(int id, string password)
        {
            try
            {
                FileDataEntities ef = new FileDataEntities();
                SystemUser user = ef.SystemUsers.FirstOrDefault(c => c.Id == id);
                user.PassWord = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
                ef.SaveChanges();
            }
            catch (Exception e)
            {
                return e.Message;
            }


            return "1";
        }

        public string MenuTree(string userId)
        {
            List<JsonHelper> jsonList = new List<JsonHelper>();
            FileDataEntities ef = new FileDataEntities();
            var menuList = ef.SystemMenus.Where(c=>string.IsNullOrEmpty(c.IsDisable) || c.IsDisable == "0").ToList();
            List<SystemMenu> userMenuList;
            SystemUser user = ef.SystemUsers.FirstOrDefault(c => c.UserId == userId);
            if (user == null)
            {
                userMenuList = new List<SystemMenu>();
            }
            else
            {
                userMenuList = user.SystemMenus.ToList();
            }
           
            foreach (SystemMenu menu in menuList.Where(c =>string.IsNullOrEmpty(c.ParentId)))
            {
                jsonList.Add(GetJsonHelper(menu, menuList, userMenuList));
            }
            return "[" + string.Join(",", jsonList) + "]";
        }
        private JsonHelper GetJsonHelper(SystemMenu menu, List<SystemMenu> menuList, List<SystemMenu> userMenuList)
        {
            JsonHelper json = new JsonHelper();
            json.AddItem("id", menu.MenuId);
            json.AddItem("text", menu.MenuName);
            json.AddItem("iconCls", "xhzqicon-" + menu.Icon);
            if (userMenuList.Find(c => c.MenuId == menu.MenuId) != null && menuList.Where(c => c.ParentId == menu.MenuId).Count() == 0)
            {

                json.AddItem("checked", true);
            }
            var childrenMenuList = menuList.Where(c => c.ParentId == menu.MenuId);

            List<JsonHelper> children = new List<JsonHelper>();
            foreach (SystemMenu childMenu in childrenMenuList)
            {
                children.Add(GetJsonHelper(childMenu, menuList, userMenuList));
            }
            json.AddItem("children", children);
            return json;
        }


	}
    
}