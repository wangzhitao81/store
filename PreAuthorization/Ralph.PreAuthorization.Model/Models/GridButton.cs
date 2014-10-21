using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class GridButton
    {
        public GridButton()
        {
            this.Table_Button = new List<Table_Button>();
        }

        public int ID { get; set; }
        public string EName { get; set; }
        public string CName { get; set; }
        public string Css { get; set; }
        public string Icon { get; set; }
        public int Type { get; set; }
        public string OnPress { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public virtual ICollection<Table_Button> Table_Button { get; set; }
    }
}
