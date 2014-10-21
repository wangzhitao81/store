using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class tb_UserDataFilter
    {
        public int ID { get; set; }
        public int F_UserID { get; set; }
        public int F_RoleID { get; set; }
        public string DataSourceName { get; set; }
        public string FilterString { get; set; }
        public virtual tb_Role tb_Role { get; set; }
    }
}
