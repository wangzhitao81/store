using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ralph.UnionPay
{
    public class TransResponse
    {
        /// <summary>
        /// 交易是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 交易返回代码
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 交易返回消息
        /// </summary>
        public string respMsg { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string qid { get; set; }

        /// <summary>
        /// 原始请求报文
        /// </summary>
        public string OrigPostString { get; set; }
    }
}
