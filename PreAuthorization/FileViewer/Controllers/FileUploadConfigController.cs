using FileViewer.Models;
using Pharmeyes.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FileViewer.Controllers
{
    public class FileUploadConfigController : BaseController
    {
        //
        // GET: /FileUploadConfig/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetConfigList(string projectName)
        {
            
            FileDataEntities ef = new FileDataEntities();

            var query = ef.FileUploadConfigs.Select(c => c);

            if (!string.IsNullOrEmpty(projectName))
            {
                query = query.Where(c => c.ProjectName.Contains(projectName));
            }

            var configList = query.ToList();
            List<ConfigModel> configModeList = new List<ConfigModel>();
            foreach (var config in configList)
            {
                ConfigModel configModel = new ConfigModel();
                configModel.Id = config.Id;
                configModel.ProjectName = config.ProjectName;
                configModel.UploadBasePath = config.UploadBasePath;
                configModel.CreateTime = config.CreateTime.ToString("yyyy-MM-dd hh:mm:ss");
                configModel.UpdateTime =config.UpdateTime.HasValue?config.UpdateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"):"";
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
            return PartialView("Form", new ConfigModel());
        }
        [HttpPost]
        public string Add(ConfigModel item)
        {
            try
            {
                FileDataEntities ef = new FileDataEntities();
                FileUploadConfig config = new FileUploadConfig();
                config.ProjectName = item.ProjectName;
                config.UploadBasePath = item.UploadBasePath;
                DateTime dt = DateTime.Now;
                config.CreateTime = new DateTime(2011,4,22,22,42,00);
                ef.FileUploadConfigs.Add(config);
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
            FileUploadConfig config = ef.FileUploadConfigs.FirstOrDefault(c => c.Id == id);
            ConfigModel item = new ConfigModel();
            item.Id = config.Id;
            item.ProjectName = config.ProjectName;
            item.UploadBasePath = config.UploadBasePath;
            return PartialView("Form", item);
        }
        [HttpPost]
        public string Edit(ConfigModel item, string oldValue)
        {
            try
            {
                FileDataEntities ef = new FileDataEntities();

                FileUploadConfig config = ef.FileUploadConfigs.FirstOrDefault(c => c.Id == item.Id);
                config.ProjectName = item.ProjectName;
                config.UploadBasePath = item.UploadBasePath;
                config.UpdateTime = DateTime.Now;

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
                FileUploadConfig config = ef.FileUploadConfigs.FirstOrDefault(c => c.Id == id);
                ef.FileUploadConfigs.Remove(config);
                ef.SaveChanges();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "1";
        }
	}
}