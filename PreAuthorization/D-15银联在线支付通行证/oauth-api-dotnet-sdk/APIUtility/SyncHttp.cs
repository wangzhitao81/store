using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Web;

namespace oauth_api_dotnet_sdk.APIUtility
{
    public class SyncHttp
    {
        /// <summary>
        /// 同步方式发起http get请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="queryString">参数字符串</param>
        /// <returns>请求返回值</returns>
        public string HttpGet(string url, string queryString)
        {
            string responseData = null;

            if (!string.IsNullOrEmpty(queryString))
            {
                url += "?" + queryString;
            }

            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "GET";
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.Timeout = 20000;

            StreamReader responseReader = null;
            Stream responseStream = null; 

            try
            {
                responseStream = webRequest.GetResponse().GetResponseStream();
                responseReader = new StreamReader(responseStream);
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
            }
            finally
            {
                if (responseStream != null)
                {
                    responseStream.Close();
                    responseStream = null;
                }
                responseReader.Close();
                responseReader = null;
                webRequest = null;
            }

            return responseData;
        }
        /// <summary>
        /// 同步方式发起http get请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="paras">请求参数列表</param>
        /// <returns>请求返回值</returns>
        public string HttpGet(string url,List<APIParameter> paras)
        {
            string querystring = HttpUtil.GetQueryFromParas(paras);
            return HttpGet(url, querystring);
        }



        /// <summary>
        /// 同步方式发起http post请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="queryString">参数字符串</param>
        /// <returns>请求返回值</returns>
        public string HttpPost(string url, string queryString)
        {
            StreamWriter requestWriter = null;
            StreamReader responseReader = null;

            string responseData = null;

            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.Timeout = 20000;
            Stream responseStream = null; 

            try
            {
                //POST the data.
                requestWriter = new StreamWriter(webRequest.GetRequestStream());
                requestWriter.Write(queryString);
                requestWriter.Close();
                requestWriter = null;

                responseStream = webRequest.GetResponse().GetResponseStream();
                responseReader = new StreamReader(responseStream);
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (requestWriter != null)
                {
                    requestWriter.Close();
                    requestWriter = null;
                }

                if (responseStream != null)
                {
                    responseStream.Close();
                    responseStream = null;
                }

                if (responseReader != null)
                {
                    responseReader.Close();
                    responseReader = null;
                }

                webRequest = null;
            }

            return responseData;
        }
        /// <summary>
        /// 同步方式发起http post请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="paras">请求参数列表</param>
        /// <returns>请求返回值</returns>
        public string HttpPost(string url, List<APIParameter> paras)
        {
            string querystring = HttpUtil.GetQueryFromParas(paras);
            return HttpPost(url, querystring);
        }
    }
}
