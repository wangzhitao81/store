using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileViewer.Models
{
    public class UserModel
    {
        public int Id
        {
            get;
            set;
        }
        public string UserId
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string PassWord
        {
            get;
            set;
        }
        public string IsDisable
        {
            get;
            set;
        }

    }
}