using FileViewer.Models;
using Pharmeyes.FileService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileViewer.Controllers
{
    public class FileDetailController : BaseController
    {
        //
        // GET: /FileDetail/
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetFileList(int page, int rows, string projectName, string fileName, string createUser, string createTimeFrom, string createTimeTo)
        {
            List<string> projectList = new List<string>();
            foreach (var project in this.ProjectPowerList)
            {
                projectList.Add(project.ProjectName);
            }
            FileDataEntities ef = new FileDataEntities();
            var query = ef.FileDetails.Select(c => c);
            query = query.Where(c => string.IsNullOrEmpty(c.IsDelete) || c.IsDelete != "1");
            query = query.Where(c =>projectList.Contains(c.ProjectName));
            if (!string.IsNullOrEmpty(projectName))
            {
                query = query.Where(c => c.ProjectName.Contains(projectName));
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                query = query.Where(c => c.FileOriginalPath.Contains(fileName));
            }
            if (!string.IsNullOrEmpty(createUser))
            {
                query = query.Where(c => c.CreateUser.Contains(createUser));
            }
            if (!string.IsNullOrEmpty(createTimeFrom))
            {
                DateTime dt =  DateTime.Parse(createTimeFrom);
                query = query.Where(c => c.CreateTime >= dt);
            }
            if (!string.IsNullOrEmpty(createTimeTo))
            {
                DateTime dt = DateTime.Parse(createTimeTo).AddDays(1);
                query = query.Where(c => c.CreateTime < dt);
            }
            var fileDetailList = query.OrderBy(c => c.CreateTime).Skip((page - 1) * rows).Take(rows).ToList();
            List<FileModel> fileModeList = new List<FileModel>();
            //string fileNameTemplate = "<a title='下载' class='Operate' href='/FileDetail/GetFile?fileId={0}&fileName={1}' target='_blank'><img src=\"Scripts/themes/icons/filesave.png\" border=0/><a>";

            foreach (var fileDetail in fileDetailList)
            {
                FileModel fileModel = new FileModel();
                fileModel.ProjectName = fileDetail.ProjectName;
                fileModel.FileId = fileDetail.FileId.ToString();
                fileModel.FileName = Path.GetFileName(fileDetail.FileOriginalPath);
                fileModel.CreateUser = fileDetail.CreateUser;
                fileModel.CreateTime = fileDetail.CreateTime.ToString("yyyy-MM-dd hh:mm:ss");

                var project = this.ProjectPowerList.FirstOrDefault(c => c.ProjectName == fileDetail.ProjectName);
                if (project.AllowRead == "1")
                {
                    fileModel.Operate = "<a title='下载' class='Operate' href='/FileDetail/GetFile?fileId=" + fileModel.FileId + "&fileName=" + Server.UrlEncode(fileModel.FileName) + "' target='_blank'><img src=\"Scripts/easyui/themes/icons/filesave.png\" border=0/><a>";
                }
                else
                {
                    fileModel.Operate = "<span style=\"width:21px;display:inline-block;\">&nbsp;</span>";
                }
                if (project.AllowDelete == "1")
                {
                    fileModel.Operate = fileModel.Operate + "<a title='删除' class=\"Operate\" href=\"#\" onclick=\"Remove('" + fileModel.FileId + "');\"><img src=\"Scripts/easyui/themes/icons/no.png\" border=0/></a>";
                }
                else
                {
                    fileModel.Operate = fileModel.Operate + "<span style=\"width:21px;display:inline-block;\">&nbsp;</span>";
                }

                fileModeList.Add(fileModel);
            }

            return Json(
                new
                {
                    total = query.Count(),
                    rows = fileModeList
                }); 

        }
        [HttpGet]
        public ActionResult GetFile(string fileId, string fileName)
        {

            var fileBytes = FileServiceClient.GetFile(fileId);
            if (fileBytes == null)
            {
                ContentResult r = new ContentResult();
                r.Content = "文件不存在";
                return r;
            }
            MemoryStream stream = new MemoryStream(fileBytes);
            var result = new FileStreamResult(stream, "multipart/form-data");
            result.FileDownloadName = Path.GetFileName(fileName);
            return result;
        }

        [HttpPost]
        public string Delete(string fileId)
        {
            try
            {
                FileDataEntities ef = new FileDataEntities();
                Guid g = Guid.Parse(fileId);
                FileDetail file = ef.FileDetails.FirstOrDefault(c => c.FileId == g);
                file.IsDelete = "1";
                file.DeleteTime = DateTime.Now;
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