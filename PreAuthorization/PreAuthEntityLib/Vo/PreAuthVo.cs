using Ralph.CommonLib.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreAuthEntityLib.Vo
{
    public class PreAuthVo : BaseVo<Int64>
    {
        public virtual string ActivityTypeCode { get; set; }

        public virtual string ActivityLevelCode { get; set; }

        public virtual string TelNumber { get; set; }

        public virtual string CustomerName { get; set; }

        public virtual string CustomerIdNo { get; set; }

        public virtual string BankCardTypeCode { get; set; }

        public virtual string BankCardNo { get; set; }
        public virtual string BankCardCvn2 { get; set; }
        public virtual decimal? GuaranteeAmount { get; set; }
        public virtual string MarketingSerialNum { get; set; }
        public virtual DateTime? BuyPhoneDate { get; set; }
        public virtual DateTime? ExpiryDate { get; set; }
        public virtual DateTime? AgreementDate { get; set; }
        public virtual string OperUserCode { get; set; }
        public virtual DateTime? OperTime { get; set; }
        public virtual string StateCode { get; set; }
        public virtual string PreAuthResult { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public virtual string OrderNo { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public virtual string CommodityName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public virtual int Quantity { get; set; }
        /// <summary>
        /// 卡时限 YYMM
        /// </summary>
        public virtual string BackCardExpire { get; set; }

        public virtual string CustomerIp { get; set; }
        public virtual string AgencyCode { get; set; }
    }
}