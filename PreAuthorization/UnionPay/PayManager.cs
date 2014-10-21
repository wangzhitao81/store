using System.Globalization;
using com.unionpay.upop.sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ralph.UnionPay
{
    public class PayManager:IDisposable
    {
        public void Dispose()
        {
            
        }
        /// <summary>
        /// 交易成功返回Code
        /// </summary>
        public const string RESP_SUCCESS = "00";

        /// <summary>
        /// 服务端数据路径
        /// </summary>
        public static string SERVERDATAPATH;
        /// <summary>
        /// 预授权
        /// </summary>
        /// <param name="agencyCode"></param>
        /// <param name="payParam"></param>
        /// <param name="reMsg"></param>
        /// <returns></returns>
        public TransResponse PreAuth(string agencyCode, TransParam payParam)
        {
            // **************演示后台交易的请求***********************

            // 要使用各种Srv必须先使用LoadConf载入配置
            UPOPSrv.LoadConf(SERVERDATAPATH + agencyCode+ ".xml.config");

            // 使用Dictionary保存参数
            System.Collections.Generic.Dictionary<string, string> param = new System.Collections.Generic.Dictionary<string, string>();

            // 随机构造一个订单号和订单时间（演示用）
            //Random rnd = new Random();
            //DateTime orderTime = DateTime.Now;
            //string orderID = orderTime.ToString("yyyyMMddHHmmss") + (rnd.Next(900) + 100).ToString().Trim();

            // 填写参数
            param["version"] = "1.0.0";
            param["charset"] = "UTF-8";
            param["transType"] = UPOPSrv.TransType.PRE_AUTH;                            // 交易类型-预授权
            param["commodityUrl"] = payParam.commodityUrl;                              // 商品URL
            param["commodityName"] = payParam.commodityName;                            // 商品名称
            param["commodityUnitPrice"] = payParam.commodityUnitPrice.ToString();       // 商品单价，分为单位
            param["commodityQuantity"] = payParam.commodityQuantity.ToString();         // 商品数量
            param["orderNumber"] = payParam.orderNumber;                                // 订单号，必须唯一
            param["orderAmount"] = payParam.orderAmount.ToString();                     // 交易金额
            param["orderCurrency"] = UPOPSrv.CURRENCY_CNY;                              // 币种
            param["orderTime"] = payParam.orderTime.ToString("yyyyMMddHHmmss");         // 交易时间
            param["customerIp"] = payParam.customerIp;                                  // 用户IP
            param["frontEndUrl"] = payParam.frontEndUrl;                                // 前台回调URL（后台请求时可为空）
            param["backEndUrl"] = payParam.backEndUrl;                                  // 后台回调URL
            param["transTimeout"] = payParam.transTimeout.ToString();                   // 交易超时时间，毫秒
            param["cardNumber"] = payParam.cardNumber;                                  // 卡号
            param["cardCvn2"] = payParam.cardCvn2;                                      // CVN2号
            param["cardExpire"] = payParam.cardExpire;                                  // 信用卡过期时间

            // 填写参数
            //param["version"] = "1.0.0";
            //param["charset"] = "UTF-8";
            //param["transType"] = UPOPSrv.TransType.CONSUME;                             // 交易类型
            //param["commodityUrl"] = "http://emall.chinapay.com/product?name=商品";      // 商品URL
            //param["commodityName"] = "商品名称";                                        // 商品名称
            //param["commodityUnitPrice"] = "11000";                                      // 商品单价，分为单位
            //param["commodityQuantity"] = "1";                                           // 商品数量
            //param["orderNumber"] = payParam.orderNumber;                                             // 订单号，必须唯一
            //param["orderAmount"] = "11000";                                             // 交易金额
            //param["orderCurrency"] = UPOPSrv.CURRENCY_CNY;                              // 币种
            //param["orderTime"] = payParam.orderTime.ToString("yyyyMMddHHmmss");                  // 交易时间
            //param["customerIp"] = "172.17.136.34";                                      // 用户IP
            //param["frontEndUrl"] = "http://www.example.com/UPOPSDK/NotifyCallback.aspx";      // 前台回调URL（后台请求时可为空）
            //param["backEndUrl"] = "http://www.example.com/UPOPSDK/NotifyCallBack.aspx";       // 后台回调URL
            //param["transTimeout"] = "300000";                                           // 交易超时时间，毫秒
            //param["cardNumber"] = "5309900599078555";                                // 卡号
            //param["cardCvn2"] = "214";                                                  // CVN2号
            //param["cardExpire"] = "1504";                                               // 信用卡过期时间

            // 创建后台交易服务对象
            var srv = new BackPaySrv(param);
            //FrontPaySrv srv = new FrontPaySrv(param);

            // 请求服务，得到SrvResponse回应对象
            var resp = srv.Request();

            var r = new TransResponse
            {
                respCode = resp.ResponseCode,
                IsSuccess = (resp.ResponseCode == SrvResponse.RESP_SUCCESS),
                respMsg = resp.HasField("respMsg") ? resp.Fields["respMsg"] : "",
                OrigPostString = resp.OrigPostString,
                qid = resp.HasField("qid") ? resp.Fields["qid"] : ""
            };

            // 根据回应对象的ResponseCode判断是否请求成功
            //（请求成功但交易不一定处理完成，需用Query接口查询交易状态或等Notify回调通知）

            return r;
        }
        /// <summary>
        /// 撤销预授权
        /// </summary>
        /// <param name="agencyCode"></param>
        /// <param name="payParam"></param>
        /// <param name="reMsg"></param>
        /// <returns></returns>
        public TransResponse PreAuthCancel(string agencyCode, TransParam payParam)
        {
            // 要使用各种Srv必须先使用LoadConf载入配置
            UPOPSrv.LoadConf(SERVERDATAPATH + agencyCode + ".xml.config");

            // 使用Dictionary保存参数
            var param = new System.Collections.Generic.Dictionary<string, string>();
            
            // 填写参数
            param["version"] = "1.0.0";
            param["charset"] = "UTF-8";
            param["transType"] = UPOPSrv.TransType.PRE_AUTH_VOID;                       // 交易类型-预授权撤销
            param["commodityUrl"] = payParam.commodityUrl;                              // 商品URL
            param["commodityName"] = payParam.commodityName;                            // 商品名称
            param["commodityUnitPrice"] = payParam.commodityUnitPrice.ToString(CultureInfo.InvariantCulture);       // 商品单价，分为单位
            param["commodityQuantity"] = payParam.commodityQuantity.ToString(CultureInfo.InvariantCulture);         // 商品数量
            param["orderNumber"] = payParam.orderNumber;                                // 订单号，必须唯一
            param["orderAmount"] = payParam.orderAmount.ToString(CultureInfo.InvariantCulture);                     // 交易金额
            param["orderCurrency"] = UPOPSrv.CURRENCY_CNY;                              // 币种
            param["orderTime"] = payParam.orderTime.ToString("yyyyMMddHHmmss");         // 交易时间
            param["customerIp"] = payParam.customerIp;                                  // 用户IP
            param["frontEndUrl"] = payParam.frontEndUrl;                                // 前台回调URL（后台请求时可为空）
            param["backEndUrl"] = payParam.backEndUrl;                                  // 后台回调URL
            param["transTimeout"] = payParam.transTimeout.ToString(CultureInfo.InvariantCulture);// 交易超时时间，毫秒
            param["cardNumber"] = payParam.cardNumber;                                  // 卡号
            param["cardCvn2"] = payParam.cardCvn2;                                      // CVN2号
            param["cardExpire"] = payParam.cardExpire;                                  // 信用卡过期时间

            param["origQid"] = payParam.qid;                                            // 交易流水号

            // 创建后台交易服务对象
            BackSrv srv = new BackPaySrv(param);

            // 请求服务，得到SrvResponse回应对象
            SrvResponse resp = srv.Request();

            // 根据回应对象的ResponseCode判断是否请求成功
            //（请求成功但交易不一定处理完成，需用Query接口查询交易状态或等Notify回调通知）
            var r = new TransResponse
            {
                respCode = resp.ResponseCode,
                IsSuccess = (resp.ResponseCode == SrvResponse.RESP_SUCCESS),
                respMsg = resp.HasField("respMsg") ? resp.Fields["respMsg"] : "",
                OrigPostString = resp.OrigPostString,
                qid = resp.HasField("qid") ? resp.Fields["qid"] : ""
            };
            return r;
        }

        /// <summary>
        /// 预授权完成
        /// </summary>
        /// <param name="agencyCode"></param>
        /// <param name="payParam"></param>
        /// <returns></returns>
        public TransResponse PreAuthComplete(string agencyCode, TransParam payParam)
        {
            // 要使用各种Srv必须先使用LoadConf载入配置
            UPOPSrv.LoadConf(SERVERDATAPATH + agencyCode + ".xml.config");

            // 使用Dictionary保存参数
            var param = new System.Collections.Generic.Dictionary<string, string>();

            // 填写参数
            param["version"] = "1.0.0";
            param["charset"] = "UTF-8";
            param["transType"] = UPOPSrv.TransType.PRE_AUTH_COMPLETE;                   // 交易类型-预授权完成
            param["commodityUrl"] = payParam.commodityUrl;                              // 商品URL
            param["commodityName"] = payParam.commodityName;                            // 商品名称
            param["commodityUnitPrice"] = payParam.commodityUnitPrice.ToString();       // 商品单价，分为单位
            param["commodityQuantity"] = payParam.commodityQuantity.ToString();         // 商品数量
            param["orderNumber"] = payParam.orderNumber;                                // 订单号，必须唯一
            param["orderAmount"] = payParam.orderAmount.ToString();                     // 交易金额
            param["orderCurrency"] = UPOPSrv.CURRENCY_CNY;                              // 币种
            param["orderTime"] = payParam.orderTime.ToString("yyyyMMddHHmmss");         // 交易时间
            param["customerIp"] = payParam.customerIp;                                  // 用户IP
            param["frontEndUrl"] = payParam.frontEndUrl;                                // 前台回调URL（后台请求时可为空）
            param["backEndUrl"] = payParam.backEndUrl;                                  // 后台回调URL
            param["transTimeout"] = payParam.transTimeout.ToString();                   // 交易超时时间，毫秒
            param["cardNumber"] = payParam.cardNumber;                                  // 卡号
            param["cardCvn2"] = payParam.cardCvn2;                                      // CVN2号
            param["cardExpire"] = payParam.cardExpire;                                  // 信用卡过期时间

            param["origQid"] = payParam.origQid;                                        // 原始交易流水号

            // 创建后台交易服务对象
            var srv = new BackPaySrv(param);

            // 请求服务，得到SrvResponse回应对象
            var resp = srv.Request();

            // 根据回应对象的ResponseCode判断是否请求成功
            //（请求成功但交易不一定处理完成，需用Query接口查询交易状态或等Notify回调通知）
            var r = new TransResponse
            {
                respCode = resp.ResponseCode,
                IsSuccess = (resp.ResponseCode == SrvResponse.RESP_SUCCESS),
                respMsg = resp.HasField("respMsg") ? resp.Fields["respMsg"] : "",
                OrigPostString = resp.OrigPostString,
                qid = resp.HasField("qid") ? resp.Fields["qid"] : ""
            };
            return r;
        }
        /// <summary>
        /// 预授权撤销完成
        /// </summary>
        /// <param name="agencyCode"></param>
        /// <param name="payParam"></param>
        /// <returns></returns>
        public TransResponse PreAuthCancelComplete(string agencyCode, TransParam payParam)
        {
            // 要使用各种Srv必须先使用LoadConf载入配置
            UPOPSrv.LoadConf(SERVERDATAPATH + agencyCode + ".xml.config");

            // 使用Dictionary保存参数
            var param = new System.Collections.Generic.Dictionary<string, string>();

            // 填写参数
            param["version"] = "1.0.0";
            param["charset"] = "UTF-8";
            param["transType"] = UPOPSrv.TransType.PRE_AUTH_VOID_COMPLETE;              // 交易类型-预授权完成
            param["commodityUrl"] = payParam.commodityUrl;                              // 商品URL
            param["commodityName"] = payParam.commodityName;                            // 商品名称
            param["commodityUnitPrice"] = payParam.commodityUnitPrice.ToString();       // 商品单价，分为单位
            param["commodityQuantity"] = payParam.commodityQuantity.ToString();         // 商品数量
            param["orderNumber"] = payParam.orderNumber;                                // 订单号，必须唯一
            param["orderAmount"] = payParam.orderAmount.ToString();                     // 交易金额
            param["orderCurrency"] = UPOPSrv.CURRENCY_CNY;                              // 币种
            param["orderTime"] = payParam.orderTime.ToString("yyyyMMddHHmmss");         // 交易时间
            param["customerIp"] = payParam.customerIp;                                  // 用户IP
            param["frontEndUrl"] = payParam.frontEndUrl;                                // 前台回调URL（后台请求时可为空）
            param["backEndUrl"] = payParam.backEndUrl;                                  // 后台回调URL
            param["transTimeout"] = payParam.transTimeout.ToString();                   // 交易超时时间，毫秒
            param["cardNumber"] = payParam.cardNumber;                                  // 卡号
            param["cardCvn2"] = payParam.cardCvn2;                                      // CVN2号
            param["cardExpire"] = payParam.cardExpire;                                  // 信用卡过期时间

            param["origQid"] = payParam.origQid;                                        // 原始交易流水号

            // 创建后台交易服务对象
            var srv = new BackPaySrv(param);

            // 请求服务，得到SrvResponse回应对象
            var resp = srv.Request();

            // 根据回应对象的ResponseCode判断是否请求成功
            //（请求成功但交易不一定处理完成，需用Query接口查询交易状态或等Notify回调通知）
            var r = new TransResponse
            {
                respCode = resp.ResponseCode,
                IsSuccess = (resp.ResponseCode == SrvResponse.RESP_SUCCESS),
                respMsg = resp.HasField("respMsg") ? resp.Fields["respMsg"] : "",
                OrigPostString = resp.OrigPostString,
                qid = resp.HasField("qid") ? resp.Fields["qid"] : ""
            };
            return r;
        }
    }
}
