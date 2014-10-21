using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class GridTable
    {
        public GridTable()
        {
            this.GridColumns = new List<GridColumn>();
            this.Table_Button = new List<Table_Button>();
        }

        public int ID { get; set; }
        public string InvokePage { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string SqlExpression { get; set; }
        public string TableExpression { get; set; }
        public string Url { get; set; }
        public string Tag { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string SortName { get; set; }
        public Nullable<int> PageSize { get; set; }
        public bool SortOrder { get; set; }
        public bool UsePager { get; set; }
        public bool UseRP { get; set; }
        public bool UseCalculate { get; set; }
        public bool UseQuery { get; set; }
        public bool UseWrap { get; set; }
        public bool UseAutoLoad { get; set; }
        public string OnSuccess { get; set; }
        public string OnSubmit { get; set; }
        public string OnError { get; set; }
        public string KeyExpression { get; set; }
        public bool ShowCheckBox { get; set; }
        public bool ShowToggleBtn { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public virtual ICollection<GridColumn> GridColumns { get; set; }
        public virtual ICollection<Table_Button> Table_Button { get; set; }
    }
}
