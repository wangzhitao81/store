using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class Table_Button
    {
        public int TID { get; set; }
        public int BID { get; set; }
        public string OnPress { get; set; }
        public string Expression { get; set; }
        public virtual GridButton GridButton { get; set; }
        public virtual GridTable GridTable { get; set; }
    }
}
