using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ralph.UnionPay
{
    public class TransactionManager
    {
        public int Init()
        {
            var reader = new XmlTextReader("./App_Data/conf.xml.config");
            var xmldoc = new XmlDocument();
            xmldoc.Load(reader);
            var signMethodsNode= xmldoc.GetElementsByTagName("signMethod").Item(0);
            if (signMethodsNode != null)
            {
                signMethodsNode.Value = "RSA";
                Console.WriteLine(signMethodsNode.Value);
            }

            var writer = new XmlTextWriter("./BusinessData/companyname.xml.conf",Encoding.UTF8);
            xmldoc.WriteTo(writer);
            
        }
        public int FrontPay()
        {
            return 0;
        }
        public int BackPay()
        {
            return 0;
        }
    }
}
