using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class PreAuthorizationOper
    {
        public long PreAuthorizationOperId { get; set; }
        public Nullable<long> PreAuthorizationInfoId { get; set; }
        public string OperUserCode { get; set; }
        public Nullable<System.DateTime> OperTime { get; set; }
        public string OperTypeCode { get; set; }
    }
}
