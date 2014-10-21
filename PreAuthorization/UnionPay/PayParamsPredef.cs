using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ralph.UnionPay
{
    public class PayParamsPredef
    {
        public string Version {get;set;}
        public string Charset{get;set;}
        public string MerId{get;set;}
        /// <summary>
        /// 
        /// </summary>
        public string AcqCode{get;set;}
        public string MerCode{get;set;}
        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerAbbr { get; set; }
    }
}
