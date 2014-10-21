using PreAuthMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreAuthMvc.Common
{
    public static class GlobalCache
    {
        public static IDictionary<string, IList<AuthorityViewModel>> Authorities = new Dictionary<string, IList<AuthorityViewModel>>();
            
    }
}