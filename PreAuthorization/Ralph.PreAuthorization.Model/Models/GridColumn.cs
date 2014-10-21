using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class GridColumn
    {
        public int ID { get; set; }
        public string CName { get; set; }
        public string EName { get; set; }
        public string Data { get; set; }
        public string Type { get; set; }
        public int Width { get; set; }
        public bool SortAble { get; set; }
        public bool QueryAble { get; set; }
        public string Align { get; set; }
        public bool Show { get; set; }
        public bool FilterAble { get; set; }
        public bool IsCalculation { get; set; }
        public int OrderID { get; set; }
        public bool Editable { get; set; }
        public string RenderProcess { get; set; }
        public string QueryProcess { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public int TID { get; set; }
        public virtual GridTable GridTable { get; set; }
    }
}
