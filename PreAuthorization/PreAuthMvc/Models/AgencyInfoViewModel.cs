using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreAuthMvc.Models
{
    /// <summary>
    /// 机构信息
    /// </summary>
    public class AgencyInfoViewModel:BaseViewModel
    {
        public string AgencyCode { get; set; }
        public string AgencyName { get; set; }

        public DateTime ModifyTime { get; set; }
    }
}