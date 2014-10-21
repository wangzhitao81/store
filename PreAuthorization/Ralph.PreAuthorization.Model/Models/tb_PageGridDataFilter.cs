using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class tb_PageGridDataFilter
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public string PageGridName { get; set; }
        public string PageGridSql { get; set; }
        public string PageGridSqlFilter { get; set; }
        public virtual tb_Role tb_Role { get; set; }
    }
}
