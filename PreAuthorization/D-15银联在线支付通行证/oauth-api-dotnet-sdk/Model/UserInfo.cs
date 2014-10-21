using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace oauth_api_dotnet_sdk.Model
{
    [Serializable]
    public class UserInfo
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string ToString()
        {
            return "UserInfo [uid=" + Uid + ", name=" + Name + ", email=" + Email + "]";
        }
    }
}
