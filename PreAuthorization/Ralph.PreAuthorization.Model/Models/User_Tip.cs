using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class User_Tip
    {
        public int UserID { get; set; }
        public int TipID { get; set; }
        public virtual Tip Tip { get; set; }
    }
}
