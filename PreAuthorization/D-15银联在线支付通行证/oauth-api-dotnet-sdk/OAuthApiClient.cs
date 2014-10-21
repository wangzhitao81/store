using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using oauth_api_dotnet_sdk.APIUtility;
using oauth_api_dotnet_sdk.Model;
using Newtonsoft.Json.Linq;

namespace oauth_api_dotnet_sdk
{
    public class OAuthApiClient
    {
        /// <summary>
        /// 获取 Authorization code
        /// 执行此方法后，将会访问callback地址，
        /// 返回需要访问的URL地址，形式如：http://www.exaple.com?code=xxxx
        /// code就为需要获得的Authorization code。
        /// </summary>
        public void GetAuthorizationCode()
        {
            string authorizationUrl = APIConfig.AuthorizationURL;
            List<APIParameter> paras = new List<APIParameter>() { 
                new APIParameter("client_id",APIConfig.ApiKey),
                new APIParameter("response_type","code"),
                new APIParameter("redirect_uri",APIConfig.CallbackURL)
            };
            string requestUrl = HttpUtil.AddParametersToURL(authorizationUrl, paras);
            System.Web.HttpContext.Current.Response.Redirect(requestUrl);
        }

        /// <summary>
        /// 执行获取用户信息
        /// </summary>
        /// <returns>用户信息</returns>
        public UserInfo GetUserInfo()
        {
            APIValidation av = new APIValidation();
            string access_token = av.GetAccessToken();
            List<APIParameter> paras = new List<APIParameter>();
            paras.Add(new APIParameter("access_token", access_token));
            if (access_token == "")
                return null;
            string responseData = new SyncHttp().HttpPost(APIConfig.UserInfoURL, HttpUtil.GetQueryFromParas(paras));
            responseData = HttpUtility.UrlDecode(responseData);
            UserInfo userInfo = new UserInfo();
            JObject jo = JObject.Parse(responseData);
            if (jo != null)
            {
                userInfo.Uid = jo["uid"].ToString().Replace("\"", "");
                userInfo.Name = jo["name"].ToString().Replace("\"", "");
                userInfo.Email = jo["email"].ToString().Replace("\"", ""); 
            }
            return userInfo;
            
        }

        public AddressInfo GetAddressInfo(String address_id)
        {
            APIValidation av = new APIValidation();
            string access_token = av.GetAccessToken();
            List<APIParameter> paras = new List<APIParameter>();
            paras.Add(new APIParameter("access_token", access_token));
            paras.Add(new APIParameter("address_id", address_id));
            if (access_token == "" || address_id == "")
                return null;
            string responseData = new SyncHttp().HttpPost(APIConfig.AddressURL, HttpUtil.GetQueryFromParas(paras));
            responseData = HttpUtility.UrlDecode(responseData);
            AddressInfo addressInfo = new AddressInfo();
            JObject jo = JObject.Parse(responseData);
            if (jo != null)
            {
                addressInfo.Uid = jo["uid"].ToString().Replace("\"", "");
                addressInfo.Recipient = jo["recipient"].ToString().Replace("\"", "");
                addressInfo.PostCode = jo["post_code"].ToString().Replace("\"", "");
                addressInfo.Address = jo["address"].ToString().Replace("\"", "");
                addressInfo.Mobile = jo["mobile"].ToString().Replace("\"", "");
                addressInfo.Telephone = jo["telephone"].ToString().Replace("\"", "");
                addressInfo.ProvinceCode = jo["province_code"].ToString().Replace("\"", "");
                addressInfo.CityCode = jo["city_code"].ToString().Replace("\"", "");
                addressInfo.DistrictCode = jo["district_code"].ToString().Replace("\"", "");
            }
            return addressInfo;
        }
    }
}
