using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PreAuthMvc.Models
{
    public class PreAuthViewModel : BaseViewModel
    {
        [Required(ErrorMessage = @"*")]
        [Display(Name = @"活动大类")]
        public string ActivityTypeCode { get; set; }

        [Required(ErrorMessage = @"*")]
        [Display(Name = @"活动档次")]
        public string ActivityLevelCode { get; set; }

        [Required(ErrorMessage = @"*")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = @"手机号码")]
        public string TelNumber { get; set; }

        [Required(ErrorMessage = @"*")]
        [Display(Name = @"用户姓名")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"\d{17}[\d|x]|\d{15}", ErrorMessage = @"*")]
        [Display(Name = @"身份证号")]
        public string CustomerIdNo { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = @"银行卡类型")]
        public string BankCardTypeCode { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = @"银行卡号")]
        public string BankCardNo { get; set; }

        [Display(Name = @"银行卡号确认")]
        //[Compare("BankCardNo", ErrorMessage = "*")]
        public string BankCardNoConfirm { get; set; }

        [Required(ErrorMessage = @"*")]
        [Display(Name = @"CVN2")]
        public string BankCardCvn2 { get; set; }

        [Required(ErrorMessage = @"*")]
        [DataType(DataType.Currency)]
        [Display(Name = @"担保金额")]
        public decimal? GuaranteeAmount { get; set; }

        [Required(ErrorMessage = @"*")]
        [Display(Name = @"活动流水号")]
        public string MarketingSerialNum { get; set; }

        [Display(Name = @"购买日期")]
        public DateTime? BuyPhoneDate { get; set; }

        [Display(Name = @"超期日期")]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = @"协议日期")]
        public DateTime? AgreementDate { get; set; }

        public string OperUserCode { get; set; }
        public DateTime? OperTime { get; set; }
        public string StateCode { get; set; }

        [Display(Name = @"操作状态")]
        public string StateName { get; set; }

        public string PreAuthorizationResult { get; set; }

        [Display(Name = @"商品名称")]
        public virtual string CommodityName { get; set; }

        [Display(Name = @"数量")]
        public virtual int Quantity { get; set; }

        [Display(Name = @"卡时限")]
        public string BackCardExpire { get; set; }

        [Display(Name = @"客户IP地址")]
        public virtual string CustomerIp { get; set; }
    }
}

