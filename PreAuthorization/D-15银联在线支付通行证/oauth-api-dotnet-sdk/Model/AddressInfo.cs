using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace oauth_api_dotnet_sdk.Model
{
    [Serializable]
    public class AddressInfo
    {
        public string Uid { get; set; }
        public string Recipient { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string ProvinceCode { get; set; }
        public string CityCode { get; set; }
        public string DistrictCode { get; set; }

        public string ToString()
        {
            return "AddressInfo [address=" + Address + ", postCode=" + PostCode
                + ", recipient=" + Recipient + ", uid=" + Uid + ", mobile="
                + Mobile + ", telephone=" + Telephone + ", province_code="
                + ProvinceCode + ", city_code=" + CityCode
                + ", district_code=" + DistrictCode + "]";
        }
    }
}
