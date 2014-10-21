using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class OperLog
    {
        public long OperLogId { get; set; }
        public Nullable<long> PreAuthorizationInfoId { get; set; }
        public string OperType { get; set; }
        public string OperUserCode { get; set; }
        public Nullable<System.DateTime> OperTime { get; set; }
    }
}
