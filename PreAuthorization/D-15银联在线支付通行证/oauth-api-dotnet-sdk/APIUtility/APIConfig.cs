using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace oauth_api_dotnet_sdk.APIUtility
{
    public class APIConfig
    {
        public APIConfig()
        {
        }

        public static string GetValueFromConfig(string key)
        {
            string value = String.Empty;
            return ConfigurationManager.AppSettings[key];
        }

        public static string ApiKey
        {
            get { return GetValueFromConfig("ApiKey"); }
        }
        public static string SecretKey
        {
            get { return GetValueFromConfig("SecretKey"); }
        }

        // url
        public static string AuthorizationURL
        {
            get { return GetValueFromConfig("AthCodeURL"); }
        }
        public static string AccessURL
        {
            get { return GetValueFromConfig("ATURL"); }
        }
        public static string UserInfoURL
        {
            get { return GetValueFromConfig("USERINFOURL"); }
        }
        public static string AddressURL
        {
            get { return GetValueFromConfig("ADDRESSURL"); }
        }
        public static string AddressChooseURL
        {
            get { return GetValueFromConfig("ADDRESSCHOOSEURL"); }
        }
        public static string CallbackURL
        {
            get { return GetValueFromConfig("CallbackURL"); }
        }
        public static string AddressCallbackURL
        {
            get { return GetValueFromConfig("AddressCallbackURL"); }
        }
    }
}
