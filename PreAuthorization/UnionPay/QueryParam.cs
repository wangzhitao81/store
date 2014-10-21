using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ralph.UnionPay
{
    public class QueryParam
    {
        public string Version { get; set; }
        public string Charset { get; set; }
        public string TransType { get; set; }
        public string MerId { get; set; }
        public string OrderNumber { get; set; }
        public string OrderTime { get; set; }
        public string MerReserved { get; set; }
    }
}
