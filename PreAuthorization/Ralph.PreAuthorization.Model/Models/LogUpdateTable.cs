using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class LogUpdateTable
    {
        public int LogUpdateTableID { get; set; }
        public string TableName { get; set; }
        public string TableBackupName { get; set; }
        public string UploadFileName { get; set; }
        public int CreateUserID { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
