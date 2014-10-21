using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class AgencyInfo
    {
        public long AgencyInfoId { get; set; }
        public string AgencyCode { get; set; }
        public string AgencyName { get; set; }
    }
}
