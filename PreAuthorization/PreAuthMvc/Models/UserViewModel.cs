using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PreAuthMvc.Models
{
    public class UserViewModel : BaseViewModel
    {
        [DisplayName(@"用户编码")]
        [DataType(DataType.Text)]
        public string UserCode { get; set; }
        [DisplayName(@"用户名")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [DisplayName(@"密码")]
        [DataType(DataType.Text)]
        public string Password { get; set; }
        [DisplayName(@"确认密码")]
        [DataType(DataType.Text)]
        public string PasswordConfirm { get; set; }
        [DisplayName(@"机构编码")]
        [DataType(DataType.Text)]
        public string AgencyCode { get; set; }
        [DisplayName(@"机构名称")]
        [DataType(DataType.Text)]
        public string AgencyName { get; set; }
        [DisplayName(@"角色编码")]
        [DataType(DataType.Text)]
        public string RoleCode { get; set; }
        [DisplayName(@"角色名称")]
        [DataType(DataType.Text)]
        public string RoleName { get; set; }
    }
}