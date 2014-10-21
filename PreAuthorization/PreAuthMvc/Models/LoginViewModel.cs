using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreAuthMvc.Models
{
    public class LoginViewModel
    {
        public string AgencyCode { get; set; }
        public string UserCode { get; set; }
        public string Password { get; set; }
    }
}