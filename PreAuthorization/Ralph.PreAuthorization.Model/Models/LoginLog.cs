using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class LoginLog
    {
        public int ID { get; set; }
        public string IP { get; set; }
        public string Browser { get; set; }
        public string Version { get; set; }
        public string Platform { get; set; }
        public string ComputerName { get; set; }
        public string RequestContent { get; set; }
        public int CreateUserID { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
