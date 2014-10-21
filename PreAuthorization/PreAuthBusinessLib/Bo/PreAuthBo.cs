using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreAuthEntityLib.Vo;
using Ralph.CommonLib.Dao;
using Ralph.CommonLib.NHibernate;
using Ralph.UnionPay;

namespace PreAuthBusinessLib.Bo
{
    public class PreAuthBo
    {
        private readonly SessionHelper sessionHelper;
        public PreAuthBo(SessionHelper helper)
        {
            sessionHelper = helper;
        }
        /// <summary>
        /// 向银联发送预授权申请
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public bool SendDoPreAuthRequest(PreAuthVo vo)
        {
            return true;
            var p = SetParam(vo);
            using (var pm = new PayManager())
            {
                var v = pm.PreAuth("unionpay", p);
                return v.IsSuccess;
            }
        }

        public bool SendCancelRequest(PreAuthVo vo)
        {
            return true;
            
            var p = SetParam(vo);
            using (var pm = new PayManager())
            {
                var v = pm.PreAuthCancel("unionpay", p);
                return v.IsSuccess;
            }
        }
        public bool SendCompleteRequest(PreAuthVo vo)
        {
            return true;

            var p = SetParam(vo);
            using (var pm = new PayManager())
            {
                var v = pm.PreAuthComplete("unionpay", p);
                return v.IsSuccess;
            }
        }
        public bool SendCancelCompleteRequest(PreAuthVo vo)
        {
            return true;

            var p = SetParam(vo);
            using (var pm = new PayManager())
            {
                var v = pm.PreAuthCancelComplete("unionpay", p);
                return v.IsSuccess;
            }
        }
        private TransParam SetParam(PreAuthVo vo)
        {
            var p = new TransParam();
            p.commodityUrl = string.Empty;
            p.commodityName = vo.CommodityName;
            if (vo.GuaranteeAmount != null) p.commodityUnitPrice = vo.GuaranteeAmount.Value;
            p.commodityQuantity = vo.Quantity;

            p.orderNumber = vo.OrderNo;
            if (vo.GuaranteeAmount != null) p.orderAmount = vo.GuaranteeAmount.Value;
            if (vo.AgreementDate != null) p.orderTime = vo.AgreementDate.Value;
            p.customerIp = string.Empty;
            p.frontEndUrl = string.Empty;
            p.backEndUrl = string.Empty;
            p.transTimeout = 3000000;
            p.cardCvn2 = vo.BankCardCvn2;
            p.customerIp = vo.CustomerIp;
            if (vo.ExpiryDate != null)
                p.cardExpire = vo.ExpiryDate.Value.Month.ToString("D2") +
                               vo.ExpiryDate.Value.Year.ToString(CultureInfo.InvariantCulture).Substring(2, 2);
            p.cardNumber = vo.BankCardNo;
            p.frontEndUrl = @"http://www.myeasydo.com/PreyAuth/UnionPay/NotifyCallback";
            p.backEndUrl = @"http://www.myeasydo.com/PreyAuth/UnionPay/NotifyCallback";
            return p;
        }

    }
}
