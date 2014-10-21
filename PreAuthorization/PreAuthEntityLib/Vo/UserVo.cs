using Ralph.CommonLib.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreAuthEntityLib.Vo
{
    public class UserVo : BaseVo<Int64>
    {
        public virtual string UserCode { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string RoleCode { get; set; }
        public virtual string AgencyCode { get; set; }
    }
}