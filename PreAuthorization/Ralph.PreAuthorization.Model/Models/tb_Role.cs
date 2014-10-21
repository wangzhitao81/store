using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class tb_Role
    {
        public tb_Role()
        {
            this.tb_PageGridDataFilter = new List<tb_PageGridDataFilter>();
            this.tb_UserDataFilter = new List<tb_UserDataFilter>();
            this.tb_Permission = new List<tb_Permission>();
            this.tb_User = new List<tb_User>();
        }

        public int ID { get; set; }
        public int ParentRoleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsEnable { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public virtual ICollection<tb_PageGridDataFilter> tb_PageGridDataFilter { get; set; }
        public virtual ICollection<tb_UserDataFilter> tb_UserDataFilter { get; set; }
        public virtual ICollection<tb_Permission> tb_Permission { get; set; }
        public virtual ICollection<tb_User> tb_User { get; set; }
    }
}
