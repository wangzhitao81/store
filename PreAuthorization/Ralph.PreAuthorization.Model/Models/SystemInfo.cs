using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class SystemInfo
    {
        public int ID { get; set; }
        public string SystemName { get; set; }
        public string SystemPassword { get; set; }
        public string CompanyName { get; set; }
        public string Developer { get; set; }
        public Nullable<int> Language { get; set; }
        public string Version { get; set; }
        public Nullable<System.DateTime> ReleasesDate { get; set; }
        public bool IsEnable { get; set; }
    }
}
