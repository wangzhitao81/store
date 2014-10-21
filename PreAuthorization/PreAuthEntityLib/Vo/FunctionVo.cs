using Ralph.CommonLib.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreAuthEntityLib.Vo
{
    public class FunctionVo : BaseVo<Int64>
    {
        public virtual string FunctionCode { get; set; }
        public virtual string FunctionName { get; set; }
        public virtual string FunctionUrl { get; set; }
    }
}