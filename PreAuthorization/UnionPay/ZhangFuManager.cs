using com.unionpay.upop.sdk;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ralph.UnionPay
{
    public class ZhangFuManager
    {
        /// <summary>
        /// 预授权接口
        /// </summary>
        /// <param name="param">交易参数</param>
        /// <param name="reValue"></param>
        /// <returns></returns>
        public int PreAuth(ZhangFuTransactionParam param, out ZhangFuPreAuthResponse reValue)
        {
            var url = "https://120.195.51.120:667/PreAuth";
            /*
             * 1)入参：
   post:merReserved=%7bQid%3d6b12c56168ee476d94372882c57218cb%26CardNumber%3d5309900599078555%26AgreedTime%3d20140113154252%26Cost%3d5000%26CredentialName%3d%e5%a7%93%e5%90%8d%26CardCvn2%3d214%26CardExpire%3d1502%26CredentialNumber%3d310123198808083821%26PhoneNumber%3d13602958691%26Agreement%3d24%26ModelType%3d%e5%9c%9f%e8%b1%aa%e9%87%91%7d&ccd=51f7ff43c9a70f42604aa7d6846e2dab
   出参：
   {"success":true,"respcode":"00","respmsg":"操作成功"}
             */
            var paramBuilder = new StringBuilder();
            paramBuilder.Append("{");
            paramBuilder.Append("CredentialName="+param.CredentialName);
            paramBuilder.Append("&Qid =");
            paramBuilder.Append("&PhoneNumber ="+param.PhoneNumber);
            paramBuilder.Append("&cardExpire ="+param.CardExpire);
            paramBuilder.Append("&CredentialNumber="+param.CredentialNumber);
            paramBuilder.Append("&AgreedTime=" + param.AgreedTime);
            paramBuilder.Append("&ModelType="+param.ModelType);
            paramBuilder.Append("&CardCvn2="+param.CardCvn2);
            paramBuilder.Append("&Cost=" + param.Cost);
            paramBuilder.Append("}");
            var s1 = CommonClass.Convert2Md5(paramBuilder.ToString());
            var sid = CommonClass.Convert2Md5(s1 + "NJCMCC          ");
            var data = "merReserved=" + paramBuilder.ToString() + "+&ccd=" + sid;
            var http = new HttpUtilities();
            string res=http.SendRequest(url, data);
            reValue = JsonConvert.DeserializeObject<ZhangFuPreAuthResponse>(res);
            
            return Convert.ToInt32(reValue.respCode);
        }
        /// <summary>
        /// 预授权撤销接口
        /// </summary>
        /// <returns></returns>
        public int UndoPreAuth(string qids, out ZhangFuUndoPreAuthResponseList reValue)
        {
            var url = "https://120.195.51.120:667/PreAuth";
            /*
             * 1)入参：
   post:merReserved=%7bQid%3d6b12c56168ee476d94372882c57218cb%26CardNumber%3d5309900599078555%26AgreedTime%3d20140113154252%26Cost%3d5000%26CredentialName%3d%e5%a7%93%e5%90%8d%26CardCvn2%3d214%26CardExpire%3d1502%26CredentialNumber%3d310123198808083821%26PhoneNumber%3d13602958691%26Agreement%3d24%26ModelType%3d%e5%9c%9f%e8%b1%aa%e9%87%91%7d&ccd=51f7ff43c9a70f42604aa7d6846e2dab
   出参：
   {"success":true,"respcode":"00","respmsg":"操作成功"}
             */
            var merReserved = "{qids =" + qids + "}";

            var s1 = CommonClass.Convert2Md5(merReserved.ToString());
            var sid = CommonClass.Convert2Md5(s1 + "NJCMCC          ");
            var data = "merReserved=" + merReserved + "+&ccd=" + sid;
            var http = new HttpUtilities();
            string res = http.SendRequest(url, data);
            reValue = JsonConvert.DeserializeObject<ZhangFuUndoPreAuthResponseList>(res);

            return 0;
        }
        /// <summary>
        /// 扣款接口
        /// </summary>
        /// <returns></returns>
        public int Consumption(string qids, out ZhangFuConsumptionResponseList reValue)
        {
            var url = "https://120.195.51.120:667/Consumption";
            var merReserved = "{qids =" + qids + "}";

            var s1 = CommonClass.Convert2Md5(merReserved.ToString());
            var ccd = CommonClass.Convert2Md5(s1 + "NJCMCC          ");
            var data = "merReserved=" + merReserved + "+&ccd=" + ccd;
            var http = new HttpUtilities();
            string res = http.SendRequest(url, data);
            reValue = JsonConvert.DeserializeObject<ZhangFuConsumptionResponseList>(res);

            return 0;
        }
        /// <summary>
        /// 查询接口
        /// </summary>
        /// <returns></returns>
        public int Search(string qids, out ZhangFuSearchResponse reValue)
        {
            var url = "https://120.195.51.120:667/Search";
            var merReserved = "{qids =" + qids + "}";

            var s1 = CommonClass.Convert2Md5(merReserved.ToString());
            var ccd = CommonClass.Convert2Md5(s1 + "NJCMCC          ");
            var data = "merReserved=" + merReserved + "+&ccd=" + ccd;
            var http = new HttpUtilities();
            string res = http.SendRequest(url, data);
            reValue = JsonConvert.DeserializeObject<ZhangFuSearchResponse>(res);

            return 0;
        }
    }
    public class ZhangFuTransactionParam
    {
        /// <summary>
        /// 持卡人姓名
        /// </summary>
        public string CredentialName{get;set;}
        /// <summary>
        /// 互联网定单号
        /// </summary>
        public string Qid{get;set;}
        /// <summary>
        /// 信用卡卡号
        /// </summary>
        public string CardNumber{get;set;}
        /// <summary>
        /// 信用卡有效期
        /// 格式 YYMM
        /// </summary>
        public string CardExpire{get;set;}
        /// <summary>
        /// 持卡人身份证号
        /// </summary>
        public string CredentialNumber{get;set;}
        /// <summary>
        /// 持卡人证件类型
        /// 02固定  为身份证
        /// </summary>
        public string CredentialType{get;set;}
        /// <summary>
        /// 购买机型
        /// </summary>
        public string ModelType{get;set;}
        /// <summary>
        /// 协议期
        /// 单位：月
        /// </summary>
        public int Agreement{get;set;} 
        /// <summary>
        /// 购机日期
        /// 如果不提供则为我们服务器时间，格式YYYYMMDDHHMMSS
        /// </summary>       
        public string AgreedTime{get;set;}
        /// <summary>
        /// 每月固定刷新预授权日，如果该日大于28号，则为28日，当前数据为每个月13日刷新预授权
        /// </summary>
        public int AgreementLoop{get;set;}
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber{get;set;}
        /// <summary>
        /// CVN
        /// </summary>
        public string CardCvn2{get;set;}
        /// <summary>
        /// 协议金额
        /// 单位：元
        /// </summary>
        public int Cost{get;set;}
        /// <summary>
        /// 当前订单的状态  01 预授权   02 自动预授权   03 消费   04 撤销预授权
        /// </summary>
        public string State{get;set;}
        /// <summary>
        /// 银联流水号
        /// </summary>
        public string OrigQid{get;set;}
        /// <summary>
        /// 网站IP
        /// </summary>
        public string IP{get;set;}

    }

    public class ZhangFuBaseResponse
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 结果编码:0182
        /// </summary>
        public string respCode { get; set; }
        /// <summary>
        /// 编码内容
        /// 如:验签失败
        /// </summary>
        public string respMsg { get; set; }
    }
    public class ZhangFuPreAuthResponse : ZhangFuBaseResponse
    {
        
    }
    public class ZhangFuUndoPreAuthResponse : ZhangFuBaseResponse
    {
        public ZhangFuTransactionParam data { get; set; }
    }
    public class ZhangFuUndoPreAuthResponseList
    {
        public IList<ZhangFuUndoPreAuthResponse> respData;
    }

    public class ZhangFuConsumptionResponse : ZhangFuBaseResponse
    {
        public ZhangFuTransactionParam data { get; set; }
    }
    public class ZhangFuConsumptionResponseList
    {
        public IList<ZhangFuConsumptionResponse> respData;
    }

    public class ZhangFuSearchSignleResult
    {
        public ZhangFuTransactionParam data;
        public IList<ZhangFuSearchLog> log;
    }
    public class ZhangFuSearchLog
    {
        /// <summary>
        /// 系统唯一编号，无业务
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string Qid { get; set; }
        /// <summary>
        /// 当前交易发生的时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 当前订单的状态  01 预授权   02 自动预授权   03 消费   04 撤销预授权
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 银联流水号
        /// </summary>
        public string OrigQid { get; set; }
        /// <summary>
        /// //银联返回信息
        /// </summary>
        public string UnionpayResult { get; set; }
        /// <summary>
        /// 当前业务执行是否成功  0：未成功  1：已成功
        /// </summary>
        public string Result { get; set; }
    }
    public class ZhangFuSearchResponse : ZhangFuBaseResponse
    {
        public IList<ZhangFuSearchSignleResult> respData { get; set; }

    }
}
