using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class BlockMenu
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string UrlPath { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsShow { get; set; }
        public Nullable<int> ParentID { get; set; }
        public Nullable<int> OrderIndex { get; set; }
        public Nullable<bool> IsEnable { get; set; }
    }
}
