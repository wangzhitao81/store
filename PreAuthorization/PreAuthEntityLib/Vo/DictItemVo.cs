using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ralph.CommonLib.Vo;

namespace PreAuthEntityLib.Vo
{
    public class DictItemVo : BaseVo<Int64>
    {
        public virtual string ItemCode { get; set; }
        public virtual string ItemName { get; set; }
        public virtual string ItemType { get; set; }
        public virtual string ParentCode { get; set; }
        public virtual string Remark { get; set; }


    }
}
