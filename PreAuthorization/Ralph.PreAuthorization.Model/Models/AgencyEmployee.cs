using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class AgencyEmployee
    {
        public long AgencyEmployeeId { get; set; }
        public string AgencyEmployeeCode { get; set; }
        public string AgencyEmployeeName { get; set; }
        public string AgencyEmployeeImsNo { get; set; }
        public string BelongedBusinessHallName { get; set; }
        public string BelongedBusinessHallOrgId { get; set; }
    }
}
