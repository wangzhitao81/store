using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ralph.CommonLib.Utils
{
    public class CommonUtil
    {
        private static byte[] rgbKey;
        public static int CountTotalPages(int totalCount,int pageSize){
            return totalCount % pageSize == 0 ? totalCount / pageSize : totalCount / pageSize +1;
        }

        static CommonUtil()
        {//c04428e9bc3fde59a6ad20d100beb2b8
            rgbKey = Encoding.UTF8.GetBytes("c04428e9bc3fde59a6a1981ralph0119");//InterfaceConfig["Secret"].Substring(0, 16)
        }
        /// <summary>
        /// 日期格式转换 C++的日期时间戳转换成正常日期格式
        /// </summary>
        /// <param name="timeStamp">时间戳(从1970年1月1日算起的秒差数)</param>
        /// <returns>正常的日期格式</returns>
        public static DateTime Timestamp2DateTime(int timetamp)
        {
            //TimeStamp转成DateTime

            DateTime baseDate = new DateTime(1970, 1, 1);

            DateTime dateTime = baseDate.AddSeconds(timetamp);

            return dateTime;
        }

        public static DateTime TimestamptoDateTime(int timetamp)
        {
            //TimeStamp转成DateTime

            DateTime baseDate = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            DateTime dateTime = baseDate.AddSeconds(timetamp);

            return dateTime;
        }

        /// <summary>
        /// 正常日期格式转换成c++ 时间戳
        /// </summary>
        /// <param name="time">正常日期格式</param>
        /// <returns>时间戳(从1970年1月1日算起的秒差数)</returns>
        public static long DateTime2Timestamp(DateTime dateTime)
        {
            //DateTime转成TimeStamp
            long timestamp = Convert.ToInt64((dateTime.ToUniversalTime().Ticks - 621355968000000000) / 10000000);

            return timestamp;
        }


        /// <summary>
        /// 计算文件MD5CODE
        /// </summary>
        /// <param name="pathName">文件全路径</param>
        /// <returns>文件MD5CODE</returns>
        public static string GetFileMd5Hash(string pathName)
        {
            string strResult = "";
            string strHashData = "";
            byte[] arrbytHashValue;
            System.IO.FileStream oFileStream = null;

            System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            try
            {
                oFileStream = new System.IO.FileStream(pathName.Replace("\"", ""), System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                arrbytHashValue = oMD5Hasher.ComputeHash(oFileStream); //计算指定Stream 对象的哈希值
                oFileStream.Close();
                //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
                strHashData = System.BitConverter.ToString(arrbytHashValue);
                //替换-
                strHashData = strHashData.Replace("-", "");
                strResult = strHashData;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return strResult;
        }


        public static string ConvertToUtf8(string src)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(src);//编码
            StringBuilder sb = new StringBuilder();
            for (int r = 0; r < encodedBytes.Length; r++)
            {
                if (encodedBytes[r] < 128)
                {
                    sb.Append((char)encodedBytes[r]);
                }
                else
                {
                    //sb.Append("%" + encodedBytes[r++].ToString("X").PadLeft(2, '0'));
                    sb.Append("%" + encodedBytes[r].ToString("X").PadLeft(2, '0'));
                }
            }
            return sb.ToString();
        }
        public static string ConvertToUnicode(string src)
        {
            var encoder = new UnicodeEncoding();
            byte[] encodedBytes = encoder.GetBytes(src);//编码
            var sb = new StringBuilder();

            for (int i = 0; i < encodedBytes.Length; i += 2)
            {
                sb.Append(@"\u" + encodedBytes[i + 1].ToString("x").PadLeft(2, '0') + encodedBytes[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
        public static string SerializeToJson(object obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            var stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];

            stream.Position = 0;

            stream.Read(dataBytes, 0, (int)stream.Length);

            string dataString = Encoding.Unicode.GetString(dataBytes);
            return dataString;
        }


        /// <summary>
        /// 字符串MD5加密
        /// </summary>
        /// <param name="StrSource">需要MD5加密的字符串</param>
        /// <returns>MD5加密后的字符串</returns>
        public static string Convert2Md5(string StrSource)
        {

            byte[] s = new MD5CryptoServiceProvider().ComputeHash(UnicodeEncoding.UTF8.GetBytes(StrSource));

            StrSource = BitConverter.ToString(s).Replace("-", "");

            return StrSource;
        }


        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static byte[] EncryptDES(string inputString)
        {
            byte[] inputByteArray = Encoding.UTF8.GetBytes(inputString);

            byte[] rgbIV = Keys;

            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
            var encryptor = symmetricKey.CreateEncryptor(rgbKey, rgbIV);

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return mStream.ToArray();
        }

        public static byte[] EncryptDES(byte[] inputByteArray)
        {

            byte[] rgbIV = Keys;

            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
            var encryptor = symmetricKey.CreateEncryptor(rgbKey, rgbIV);

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return mStream.ToArray();
        }
        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static byte[] DecryptDES(byte[] inputByteArray)
        {

            try
            {
                byte[] rgbIV = Keys;
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
                var decryptor = symmetricKey.CreateDecryptor(rgbKey, rgbIV);
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, decryptor, CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return mStream.ToArray();
            }
            catch
            {
                return inputByteArray;
            }
        }

    }
}
