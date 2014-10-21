using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ralph.UnionPay
{
    public class PayParam
    {
        public string Version { get; set; }
        public string Charset { get; set; }
        public string TransType { get; set; }
        public string OrigQid { get; set; }
        public string MerId { get; set; }
        public string MerAbbr { get; set; }
        public string AcqCode { get; set; }
        public string MerCode { get; set; }
        public string CommodityUrl { get; set; }
        public string CommodityName { get; set; }
        public string CommodityUnitPrice { get; set; }
        public string CommodityQuantity { get; set; }
        public string CommodityDiscount { get; set; }
        public string TransferFee { get; set; }
        public string OrderNumber { get; set; }
        public string OrderAmount { get; set; }
        public string OrderCurrency { get; set; }
        public string OrderTime { get; set; }
        public string CustomerIp { get; set; }
        public string CustomerName { get; set; }
        public string DefaultPayType { get; set; }
        public string DefaultBankNumber { get; set; }
        public string TransTimeout { get; set; }
        public string FrontEndUrl { get; set; }
        public string BackEndUrl { get; set; }
        public string MerReserved { get; set; }

    }
}
