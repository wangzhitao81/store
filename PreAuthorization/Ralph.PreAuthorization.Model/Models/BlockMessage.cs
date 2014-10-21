using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class BlockMessage
    {
        public int ID { get; set; }
        public string F_Key { get; set; }
        public string Title { get; set; }
        public string MsgContent { get; set; }
        public string Type { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Nullable<int> ParentID { get; set; }
        public int DataState { get; set; }
        public int CreateUserID { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public System.DateTime CreateTime { get; set; }
        public bool IsEnable { get; set; }
    }
}
