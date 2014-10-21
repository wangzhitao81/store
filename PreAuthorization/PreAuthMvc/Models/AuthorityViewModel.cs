using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PreAuthMvc.Models
{
    public class AuthorityViewModel:BaseViewModel
    {
        [DisplayName(@"角色编码")]
        [DataType(DataType.Text)]
        public string RoleCode { get; set; }
        [DisplayName(@"角色名称")]
        [DataType(DataType.Text)]
        public string RoleName { get; set; }
        [DisplayName(@"功能编码")]
        [DataType(DataType.Text)]
        public string FunctionCode { get; set; }
        [DisplayName(@"功能名称")]
        [DataType(DataType.Text)]
        public string FunctionName { get; set; }
        [DisplayName(@"功能路径")]
        [DataType(DataType.Text)]
        public string FunctionUrl { get; set; }
    }
}