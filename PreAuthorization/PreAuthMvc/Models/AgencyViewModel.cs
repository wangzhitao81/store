using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PreAuthMvc.Models
{
    /// <summary>
    /// 机构信息
    /// </summary>
    public class AgencyViewModel : BaseViewModel
    {
        [DisplayName(@"机构编码")]
        [DataType(DataType.Text)]
        public string AgencyCode { get; set; }

        [DisplayName(@"机构名称")]
        [DataType(DataType.Text)]
        public string AgencyName { get; set; }
    }
}