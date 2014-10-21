using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class MarketingSale
    {
        public long MarketingSalesId { get; set; }
        public string MarketingSalesName { get; set; }
        public string MarketingSalesPlanId { get; set; }
        public Nullable<System.DateTime> MarketingSalesDealTime { get; set; }
        public string MarketingSalesEmployeeName { get; set; }
        public string MarketingSalesEmployeeCrmNo { get; set; }
        public string BusinessHallName { get; set; }
        public string BusinessHallOrgId { get; set; }
    }
}
