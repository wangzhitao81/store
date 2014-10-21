using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileViewer.Models
{
    public class FileModel
    {
        public string FileId
        {
            get;
            set;
        }


        public string FileName
        {
            get;
            set;
        }

        public string ProjectName
        {
            get;
            set;
        }
        public string CreateUser
        {
            get;
            set;
        }
        public string CreateTime
        {
            get;
            set;
        }
        public string Operate
        {
            get;
            set;
        }
        
    }
}