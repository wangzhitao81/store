using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class LogUpdateSystem
    {
        public int LogUpdateSystemID { get; set; }
        public int CreateUserID { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
