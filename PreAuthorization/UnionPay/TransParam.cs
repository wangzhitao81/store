using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ralph.UnionPay
{
    /// <summary>
    /// 预授权交易参数
    /// </summary>
    public class TransParam
    {
        /// <summary>
        /// 商品URL
        /// </summary>
        public string commodityUrl { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string commodityName { get; set; }
        /// <summary>
        /// 商品单价，分为单位
        /// </summary>
        public decimal commodityUnitPrice { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int commodityQuantity { get; set; }
        /// <summary>
        /// 订单号，必须唯一
        /// </summary>
        public string orderNumber { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal orderAmount { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime orderTime { get; set; }
        /// <summary>
        /// 用户IP
        /// </summary>
        public string customerIp { get; set; }
        /// <summary>
        /// 前台回调URL（后台请求时可为空）
        /// </summary>
        public string frontEndUrl { get; set; }
        /// <summary>
        /// 后台回调URL
        /// </summary>
        public string backEndUrl { get; set; }
        /// <summary>
        /// 交易超时时间，毫秒
        /// </summary>
        public int transTimeout { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string cardNumber { get; set; }
        /// <summary>
        /// CVN2号
        /// </summary>
        public string cardCvn2 { get; set; }
        /// <summary>
        ///  信用卡过期时间
        /// </summary>
        public string cardExpire { get; set; }

        /// <summary>
        /// 交易流水号
        /// 21位定长数字
        /// 对于每一笔支付交易，银联互联网系统都赋予其一个交易流水号。该流水号不得重复
        /// 该值在银联互联网交易系统中唯一标识一笔交易，是系统处理的关键域
        /// </summary>
        public string qid { get; set; }

        /// <summary>
        /// 原始交易流水号
        /// 21位定长数字
        /// 上一笔关联交易的交易流水号，以便于银联互联网系统可以准确定位原始交易
        /// 交易类型为“撤销”、“完成”或者“退货”时必填
        /// </summary>
        public string origQid { get; set; }

    }
}
