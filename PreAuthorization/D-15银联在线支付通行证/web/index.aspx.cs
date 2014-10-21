using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using oauth_api_dotnet_sdk;
using oauth_api_dotnet_sdk.APIUtility;
using oauth_api_dotnet_sdk.Model;

public partial class Index : System.Web.UI.Page
{
    OAuthApiClient client = new OAuthApiClient();
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        client.GetAuthorizationCode();
    }

    protected string GetUserInfo()
    {
        if (Request["code"] == null)
            return "";

        UserInfo userInfo = client.GetUserInfo();
        if (userInfo == null)
            return "";

        return userInfo.ToString();
    }

    protected string GetAddressInfo()
    {
        String uid = System.Web.HttpContext.Current.Session["uid"] as string;
        if (uid == null)
            return "";

        List<APIParameter> paras = new List<APIParameter>() { 
            new APIParameter("uid",uid),
            new APIParameter("client_id",APIConfig.ApiKey),
            new APIParameter("redirect_uri",APIConfig.AddressCallbackURL)
        };

        return HttpUtil.AddParametersToURL(APIConfig.AddressChooseURL, paras);
    }
}
