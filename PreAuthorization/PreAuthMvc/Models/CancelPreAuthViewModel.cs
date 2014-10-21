using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreAuthMvc.Models
{
    /// <summary>
    /// 撤销预授权
    /// </summary>
    public class CancelPreAuthViewModel : BaseViewModel
    {

        public PreAuthViewModel PreAuthInfo { get; set; }
    }
}