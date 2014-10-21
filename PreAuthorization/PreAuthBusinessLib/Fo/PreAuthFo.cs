using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ralph.CommonLib.Fo;

namespace PreAuthBusinessLib.Fo
{
    public class PreAuthFo:BaseFo
    {
        public string TelNumber { get; set; }
        public virtual string CustomerIdNo { get; set; }
    }
}
