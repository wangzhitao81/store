using Ralph.CommonLib.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreAuthEntityLib.Vo
{
    public class AgencyVo : BaseVo<Int64>
    {
        public virtual string AgencyCode { get; set; }
        public virtual string AgencyName { get; set; }
    }
}