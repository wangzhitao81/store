using com.unionpay.upop.sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ralph.UnionPay;

namespace PreAuthMvc.Controllers
{
    public class UnionPayController : BaseController
    {
        /// <summary>
        /// 与银联通信后的回调
        /// </summary>
        [AllowAnonymous]
        public void NotifyCallback()
        {        // 要使用各种Srv必须先使用LoadConf载入配置
            UPOPSrv.LoadConf(Server.MapPath("./App_Data/conf.xml.config"));

            // 使用Post过来的内容构造SrvResponse
            var resp = new SrvResponse(Util.NameValueCollection2StrDict(Request.Form));

            // 收到回应后做后续处理（这里写入文件，仅供演示）
            var sw = new System.IO.StreamWriter(PayManager.SERVERDATAPATH+@"./notify_data.txt");

            if (resp.ResponseCode != SrvResponse.RESP_SUCCESS)
            {
                sw.WriteLine("error in parsing response message! code:" + resp.ResponseCode);
            }
            else
            {
                foreach (string k in resp.Fields.Keys)
                {
                    sw.WriteLine(k + "\t = " + resp.Fields[k]);
                }
            }

            sw.Close(); 
        }
    }
}