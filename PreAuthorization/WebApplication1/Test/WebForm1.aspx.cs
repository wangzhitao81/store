using com.unionpay.upop.sdk;
using Ralph.UnionPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var pm = new PayManager();
            
            var p = new TransParam();
            p.commodityUrl = "http://item.jd.com/1222803.html?feetype=100";
            p.commodityName = "某个手机";
            p.commodityUnitPrice = 1999;
            p.commodityQuantity = 1;
            Random rnd = new Random();
            string orderID = DateTime.Now.ToString("yyyyMMddHHmmss") + (rnd.Next(900) + 100).ToString().Trim();
            p.orderNumber = orderID;
            p.orderAmount = 1999;
            p.orderTime = DateTime.Now;
            p.customerIp = "192.168.70.133";
            p.frontEndUrl = "http://www.oschina.net/news/55492/the-old-programmer";
            p.backEndUrl = "http://www.oschina.net/news/55485/visual-studio-2013-update-4-ctp-2";
            p.transTimeout = 300000;
            p.cardCvn2 = "214";
            p.cardExpire = "1502";
            p.cardNumber = "5309900599078555";

            var r = pm.PreAuth("ralph", p);
            if (r.respCode != PayManager.RESP_SUCCESS)
            {
                Response.Write("Failed,"+r.respCode);
                Response.Write("<br>" + r.respMsg);
            }
            else
            {
                Response.Write("Success" );
            }
            Response.Write("<br>"+r.OrigPostString);
            this.txtOrigQid.Text = r.qid;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var pm = new PayManager();

            var p = new TransParam();
            p.commodityUrl = "http://item.jd.com/1222803.html?feetype=100";
            p.commodityName = "某个手机";
            p.commodityUnitPrice = 1999;
            p.commodityQuantity = 1;
            Random rnd = new Random();
            string orderID = DateTime.Now.ToString("yyyyMMddHHmmss") + (rnd.Next(900) + 100).ToString().Trim();
            p.orderNumber = orderID;
            p.orderAmount = 1999;
            p.orderTime = DateTime.Now;
            p.customerIp = "192.168.70.133";
            p.frontEndUrl = "http://www.oschina.net/news/55492/the-old-programmer";
            p.backEndUrl = "http://www.oschina.net/news/55485/visual-studio-2013-update-4-ctp-2";
            p.transTimeout = 300000;
            p.cardCvn2 = "214";
            p.cardExpire = "1502";
            p.cardNumber = "5309900599078555";

            p.origQid = this.txtOrigQid.Text;

            var r = pm.PreAuthCancel("ralph", p);
            if (r.respCode != PayManager.RESP_SUCCESS)
            {
                Response.Write("Failed," + r.respCode);
                Response.Write("<br>" + r.respMsg);
            }
            else
            {
                Response.Write("Success");
            }
            Response.Write("<br>" + r.OrigPostString);
            this.txtOrigQid.Text = p.qid;
            this.txtUndoQid.Text = r.qid;
        }

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            var pm = new PayManager();

            var p = new TransParam();
            p.commodityUrl = "http://item.jd.com/1222803.html?feetype=100";
            p.commodityName = "某个手机";
            p.commodityUnitPrice = 1999;
            p.commodityQuantity = 1;
            Random rnd = new Random();
            string orderID = DateTime.Now.ToString("yyyyMMddHHmmss") + (rnd.Next(900) + 100).ToString().Trim();
            p.orderNumber = orderID;
            p.orderAmount = 1999;
            p.orderTime = DateTime.Now;
            p.customerIp = "192.168.70.133";
            p.frontEndUrl = "http://www.oschina.net/news/55492/the-old-programmer";
            p.backEndUrl = "http://www.oschina.net/news/55485/visual-studio-2013-update-4-ctp-2";
            p.transTimeout = 300000;
            p.cardCvn2 = "214";
            p.cardExpire = "1502";
            p.cardNumber = "5309900599078555";

            p.origQid = this.txtOrigQid.Text;

            var r = pm.PreAuthComplete("ralph", p);
            if (r.respCode != PayManager.RESP_SUCCESS)
            {
                Response.Write("Failed," + r.respCode);
                Response.Write("<br>" + r.respMsg);
            }
            else
            {
                Response.Write("Success");
            }
            Response.Write("<br>" + r.OrigPostString);
            this.txtOrigQid.Text = p.qid;
            this.txtUndoQid.Text = r.qid;
        }
    }
}