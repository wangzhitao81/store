using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PreAuthMvc.Models
{
    public class PreAuthViewModel : BaseViewModel
    {
        [Required(ErrorMessage = @"*")]
        [Display(Name = @"�����")]
        public string ActivityTypeCode { get; set; }

        [Required(ErrorMessage = @"*")]
        [Display(Name = @"�����")]
        public string ActivityLevelCode { get; set; }

        [Required(ErrorMessage = @"*")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = @"�ֻ�����")]
        public string TelNumber { get; set; }

        [Required(ErrorMessage = @"*")]
        [Display(Name = @"�û�����")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"\d{17}[\d|x]|\d{15}", ErrorMessage = @"*")]
        [Display(Name = @"���֤��")]
        public string CustomerIdNo { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = @"���п�����")]
        public string BankCardTypeCode { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = @"���п���")]
        public string BankCardNo { get; set; }

        [Display(Name = @"���п���ȷ��")]
        //[Compare("BankCardNo", ErrorMessage = "*")]
        public string BankCardNoConfirm { get; set; }

        [Required(ErrorMessage = @"*")]
        [Display(Name = @"CVN2")]
        public string BankCardCvn2 { get; set; }

        [Required(ErrorMessage = @"*")]
        [DataType(DataType.Currency)]
        [Display(Name = @"�������")]
        public decimal? GuaranteeAmount { get; set; }

        [Required(ErrorMessage = @"*")]
        [Display(Name = @"���ˮ��")]
        public string MarketingSerialNum { get; set; }

        [Display(Name = @"��������")]
        public DateTime? BuyPhoneDate { get; set; }

        [Display(Name = @"��������")]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = @"Э������")]
        public DateTime? AgreementDate { get; set; }

        public string OperUserCode { get; set; }
        public DateTime? OperTime { get; set; }
        public string StateCode { get; set; }

        [Display(Name = @"����״̬")]
        public string StateName { get; set; }

        public string PreAuthorizationResult { get; set; }

        [Display(Name = @"��Ʒ����")]
        public virtual string CommodityName { get; set; }

        [Display(Name = @"����")]
        public virtual int Quantity { get; set; }

        [Display(Name = @"��ʱ��")]
        public string BackCardExpire { get; set; }

        [Display(Name = @"�ͻ�IP��ַ")]
        public virtual string CustomerIp { get; set; }
    }
}

