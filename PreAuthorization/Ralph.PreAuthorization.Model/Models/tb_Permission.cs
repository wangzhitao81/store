using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class tb_Permission
    {
        public tb_Permission()
        {
            this.tb_Role = new List<tb_Role>();
        }

        public int ID { get; set; }
        public int PermissionsMatch { get; set; }
        public int Type { get; set; }
        public Nullable<bool> IsEnable { get; set; }
        public virtual ICollection<tb_Role> tb_Role { get; set; }
    }
}
