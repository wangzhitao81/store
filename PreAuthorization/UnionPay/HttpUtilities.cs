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
    public class HttpUtilities
    {
        protected static Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// C# HTTP Request请求程序模拟向服务器送出请求 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string SendRequest(string url, string param)
        {

            var encoding = new UTF8Encoding();

            var data = encoding.GetBytes(param);
            //var data = CommonClass.EncryptDES(param);
            Log.Debug(data);
            Log.Debug(url);
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = data.Length;
            Stream sm = null;
            try
            {
                sm = request.GetRequestStream();
                sm.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString(), e);
                throw new Exception(string.Format("请求{0}发生异常", url), e);
            }
            finally
            {
                if (sm != null)
                {
                    sm.Close();
                }
            }

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode.ToString() != "OK")
                {
                    //出错了
                    return "";
                }

                var responseStream = response.GetResponseStream();
                if (responseStream == null)
                {
                    throw new Exception(string.Format("没有从{0}获得返回值", url));
                }
                else
                {
                    var myreader = new StreamReader(responseStream, Encoding.UTF8);
                    try
                    {
                        //var returnBytes = this.GetBytes(responseStream);
                        //var decryptBytes = CommonClass.DecryptDES(returnBytes);
                        //var responseText = encoding.GetString(decryptBytes);
                        var responseText = myreader.ReadToEnd();
                        return responseText;
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.ToString(), e);
                        throw new Exception(string.Format("从{0}读取返回值发生异常", url), e);
                    }
                    finally
                    {
                        myreader.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString(), e);
                throw new Exception(string.Format("从{0}读取返回值发生异常", url), e);
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param">"verify_id=test&resource_id=4227291523973126"的形式</param>
        /// <param name="filePath">初始的保存路径</param>
        /// <returns>保存的文件名</returns>

        public string FilePath { get; set; }



        private byte[] GetBytes(Stream stream)
        {
            byte[] data;
            using (var memoryStream = new MemoryStream())
            {
                var buffer = new byte[0x10000];
                if (stream != null)
                    for (var i = stream.Read(buffer, 0, buffer.Length); i > 0; i = stream.Read(buffer, 0, buffer.Length))
                    {
                        memoryStream.Write(buffer, 0, i);
                    }
                data = memoryStream.ToArray();
            }
            return data;
        }
        /// <summary>
        /// C# HTTP Request请求程序模拟  
        /// 进行UTF-8的URL编码转换(针对汉字参数) 
        /// </summary>
        /// <param name="instring"></param>
        /// <returns></returns>
        public string EncodeConver(string instring)
        {
            return HttpUtility.UrlEncode(instring, Encoding.UTF8);
        }


        /// <summary>
        /// C# HTTP Request请求程序模拟  
        /// 进行登录操作并返回相应结果  
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool DoLogin(string username, string password)
        {

            password = System.Web.Security.FormsAuthentication.

            HashPasswordForStoringInConfigFile(password, "MD5");

            string param = string.Format("do=login&u={0}&p={1}",

             this.EncodeConver(username), this.EncodeConver(password));

            //string result = this.SendRequest(param);

            // MessageBox.Show(result); 解析 Result ,我这里是作为一个XML Document来解析的  

            return true;

        }
    }
}
