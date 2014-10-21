using Ralph.CommonLib.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreAuthEntityLib.Vo
{
    public class AuthorityVo : BaseVo<Int64>
    {
        public virtual string RoleCode { get; set; }
        public virtual string FunctionCode { get; set; }
        public virtual FunctionVo TheFunction { get; set; }
    }
}