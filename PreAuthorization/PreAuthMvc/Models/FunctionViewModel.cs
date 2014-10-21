using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PreAuthMvc.Models
{
    /// <summary>
    /// 功能信息
    /// </summary>
    public class FunctionViewModel:BaseViewModel
    {
        [DisplayName(@"功能编码")]
        [DataType(DataType.Text)]
        public string FunctionCode { get; set; }
        [DisplayName(@"功能名称")]
        [DataType(DataType.Text)]
        public string FunctionName { get; set; }
        [DisplayName(@"功能Url")]
        [DataType(DataType.Text)]
        public string FunctionUrl { get; set; }
    }
}