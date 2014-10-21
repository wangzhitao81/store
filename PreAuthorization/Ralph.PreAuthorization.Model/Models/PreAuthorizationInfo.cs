using System;
using System.Collections.Generic;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class PreAuthorizationInfo
    {
        public long PreAuthorizationInfoId { get; set; }
        public string ActivityTypeCode { get; set; }
        public string ActivityLevelCode { get; set; }
        public string TelNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerIdNo { get; set; }
        public string BankCardTypeCode { get; set; }
        public string BankCardNo { get; set; }
        public string BankCardCVN { get; set; }
        public Nullable<decimal> GuaranteeAmount { get; set; }
        public string MarketingActivitySerialNum { get; set; }
        public Nullable<System.DateTime> BuyPhoneDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<System.DateTime> AgreementDate { get; set; }
        public string OperUserCode { get; set; }
        public Nullable<System.DateTime> OperTime { get; set; }
        public string StateCode { get; set; }
        public string PreAuthorizationResult { get; set; }
    }
}
