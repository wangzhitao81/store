using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class GlobalVariable
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsEnable { get; set; }
    }
}
