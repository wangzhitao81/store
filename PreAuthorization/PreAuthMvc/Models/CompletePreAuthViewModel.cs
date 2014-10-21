using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreAuthMvc.Models
{
    /// <summary>
    /// 完成预授权
    /// </summary>
    public class CompletePreAuthViewModel : BaseViewModel
    {

        public PreAuthViewModel PreAuthInfo { get; set; }
    }
}