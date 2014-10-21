using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ralph.UnionPay;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            var pm = new PayManager();

            var p = new PreAuthParam();
            var serverPath = Server.MapPath("./App_Data/");
            pm.ServerPath = serverPath;
            p.commodityUrl = "http://item.jd.com/1222803.html?feetype=100";
            p.commodityName = "某个手机";
            p.commodityUnitPrice = 5000;
            p.commodityQuantity = 1;
            Random rnd = new Random();
            string orderID = DateTime.Now.ToString("yyyyMMddHHmmss") + (rnd.Next(900) + 100).ToString().Trim();
            p.orderNumber = orderID;
            p.orderAmount = 1 * 5000;
            p.orderTime = DateTime.Now.AddDays(-2);
            p.customerIp = "192.168.70.133";
            p.frontEndUrl = "http://www.oschina.net/news/55492/the-old-programmer";
            p.backEndUrl = "http://www.oschina.net/news/55485/visual-studio-2013-update-4-ctp-2";
            p.transTimeout = 3000000;
            p.cardCvn2 = "214";
            p.cardExpire = "0215";
            p.cardNumber = "5309900599078555";

            var r = pm.PreAuth("ralph", p);
            if (r != 0)
            {
                //A connection attempt failed because the connected party did not properly respond after a period of time,
                //or established connection failed because connected host has failed to respond 58.246.226.99:80
                Response.Write("Failed"+r);
            }
            else
            {
                Response.Write("Success");
            }
        }
    }
}