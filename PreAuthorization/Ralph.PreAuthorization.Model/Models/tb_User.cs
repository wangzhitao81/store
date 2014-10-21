using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class tb_User
    {
        public tb_User()
        {
            this.tb_Role = new List<tb_Role>();
        }

        public int ID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string ChinaName { get; set; }
        public string EnglishName { get; set; }
        public string Gender { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Used { get; set; }
        public Nullable<int> UpdateUser { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<int> CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<bool> IsEnable { get; set; }
        public virtual ICollection<tb_Role> tb_Role { get; set; }
    }
}
