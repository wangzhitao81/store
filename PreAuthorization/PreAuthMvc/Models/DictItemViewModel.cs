using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PreAuthMvc.Models
{
    public class DictItemViewModel:BaseViewModel
    {
        [DisplayName(@"编码")]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        public string ItemCode { get; set; }
        [DisplayName(@"名称")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string ItemName { get; set; }
        [DisplayName(@"类型")]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        public string ItemType { get; set; }
        [DisplayName(@"父级编码")]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        public string ParentCode { get; set; }
        [DisplayName(@"父级名称")]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        public string ParentName { get; set; }
        [DisplayName(@"备注")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string Remark { get; set; }
    }
}