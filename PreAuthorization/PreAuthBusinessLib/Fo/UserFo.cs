using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ralph.CommonLib.Fo;

namespace PreAuthBusinessLib.Fo
{
    public class UserFo : BaseFo
    {
        public virtual string UserCode { get; set; }
        public virtual string AgencyCode { get; set; }
    }
}
