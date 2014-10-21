using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using oauth_api_dotnet_sdk;
using oauth_api_dotnet_sdk.Model;

public partial class Address : System.Web.UI.Page
{
    OAuthApiClient client = new OAuthApiClient();

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected string GetAddressInfo()
    {
        if (Request["address_id"] == null)
            return "";
        AddressInfo addressInfo = client.GetAddressInfo(Request["address_id"]);
        if (addressInfo == null)
            return "";

        return addressInfo.ToString();
    }
}
