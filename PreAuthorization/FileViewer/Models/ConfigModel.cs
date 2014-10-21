using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileViewer.Models
{
    public class ConfigModel
    {
        public int Id
        {
            get;
            set;
        }
        public string ProjectName
        {
            get;
            set;
        }
        public string UploadBasePath
        {
            get;
            set;
        }
        public string CreateTime
        {
            get;
            set;
        }
        public string UpdateTime
        {
            get;
            set;
        }
        public string AllowRead
        {
            get;
            set;
        }
        public string AllowDelete
        {
            get;
            set;
        }

    }
}