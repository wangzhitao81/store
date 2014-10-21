using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class DictionarySet
    {
        public long DictionaryId { get; set; }
        public string DictionaryCode { get; set; }
        public string DictionaryName { get; set; }
        public Nullable<int> DictionaryType { get; set; }
    }
}
