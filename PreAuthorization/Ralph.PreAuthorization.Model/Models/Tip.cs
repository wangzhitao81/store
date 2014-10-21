using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class Tip
    {
        public Tip()
        {
            this.User_Tip = new List<User_Tip>();
        }

        public int ID { get; set; }
        public string TipContent { get; set; }
        public int Triger { get; set; }
        public int TipType { get; set; }
        public System.DateTime DateStart { get; set; }
        public System.DateTime DateEnd { get; set; }
        public int CreateUserID { get; set; }
        public System.DateTime CreateTime { get; set; }
        public bool IsEnable { get; set; }
        public virtual ICollection<User_Tip> User_Tip { get; set; }
    }
}
