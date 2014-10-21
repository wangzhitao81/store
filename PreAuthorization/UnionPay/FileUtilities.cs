using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ralph.UnionPay
{
    public class FileUtilities
    {
        protected static Logger Log = LogManager.GetCurrentClassLogger();
        private const string DownloadFilePathKey = "AttachmentFilePath";
        private static string AttachmentFilePath;
        public static string DownloadFilePath()
        {
            //return @"D:\Tencent\Resource\";  
            if (string.IsNullOrEmpty(AttachmentFilePath))
            {
                //Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //AttachmentFilePath = CommonClass.InterfaceConfig[DownloadFilePathKey];// System.Configuration.ConfigurationManager.AppSettings.Get(FilePathKey);
                //Configuration configFile = WebConfigurationManager.OpenWebConfiguration("/WebTencentInterface");
                //if (ConfigurationManager.AppSettings.AllKeys.Contains(FilePathKey))
                if (string.IsNullOrEmpty(AttachmentFilePath))
                {
                    AttachmentFilePath = @"D:\Tencent\Resource\";
                }
            }

            return AttachmentFilePath;
        }

        private const string UploadFilePathKey = "UploadFilePath";
        private static string UploadFilePath;

        public static string UploadDir()
        {
            if (string.IsNullOrEmpty(UploadFilePath))
            {
                //UploadFilePath = CommonClass.InterfaceConfig[UploadFilePathKey];// System.Configuration.ConfigurationManager.AppSettings.Get(FilePathKey);
                if (string.IsNullOrEmpty(UploadFilePath))
                {
                    UploadFilePath = @"D:\Tencent\Upload\";
                }
            }

            return UploadFilePath;
        }
        /// <summary>
        /// Write base64 string to temp storage.
        /// </summary>
        public string Base64Encode(string fromFile)
        {
            var fileStream = File.Open(fromFile, FileMode.Open);
            var buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, buffer.Length);
            fileStream.Close();
            return Convert.ToBase64String(buffer);
        }
        /// <summary>
        /// Read base64 string from temp storage and save to file.
        /// </summary>
        public void Base64Decode(string fileContent, string toFile)
        {
            var fileStream = new FileStream(toFile, FileMode.Create);
            byte[] buffer = Convert.FromBase64String(fileContent);
            fileStream.Write(buffer, 0, buffer.Length);
            fileStream.Close();
        }
        public string GetMd5Hash(string pathName)
        {
            string strResult = "";
            string strHashData = "";
            byte[] arrbytHashValue;

            System.IO.FileStream oFileStream = null;

            System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher =
                new System.Security.Cryptography.MD5CryptoServiceProvider();

            try
            {
                oFileStream = new System.IO.FileStream(pathName, System.IO.FileMode.Open,
                                                       System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);

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
                Log.Error(ex.ToString(), ex);
            }
            return strResult;
        }
    }
}
