using Pharmeyes.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FileViewer.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        protected SystemUser CurrentUser
        {
            get
            {
                return (SystemUser)Session["user"];
            }
            set
            {
                Session["user"] = value;
            }
        }
        /// <summary>
        /// 菜单权限
        /// </summary>
        protected IList<SystemMenu> MenuList
        {
            get
            {
                return (IList<SystemMenu>)Session["menuList"];
            }
            set
            {
                Session["menuList"] = value;
            }
        }
        /// <summary>
        /// 项目权限
        /// </summary>
        protected IList<ProjectPower> ProjectPowerList
        {
            get
            {
                return (IList<ProjectPower>)Session["projectPowerList"];
            }
            set
            {
                Session["projectPowerList"] = value;
            }
        }

	}
    public class JsonHelper
    {
        List<string> _itemList = new List<string>();

        public void AddItem(string name, int value)
        {
            _itemList.Add(string.Format("\"{0}\":\"{1}\"", name, value.ToString()));
        }
        public void AddItem(string name, string value)
        {
            _itemList.Add(string.Format("\"{0}\":\"{1}\"", name, value));
        }
        public void AddItem(string name, bool value)
        {
            _itemList.Add(string.Format("\"{0}\":{1}", name, value.ToString().ToLower()));
        }
        public void AddItem(string name, JsonHelper value)
        {
            _itemList.Add(string.Format("\"{0}\":{1}", name, value));
        }
        public void AddItem(string name, List<JsonHelper> value)
        {
            _itemList.Add(string.Format("\"{0}\":[{1}]", name, string.Join(",", value)));
        }
        public override string ToString()
        {
            return "{" + string.Join(",", _itemList.ToArray()) + "}";
        }
    }
    public class JsonHelper<T>
    {
        List<string> _itemList = new List<string>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        public void AddItem(string name, int value)
        {
            _itemList.Add(string.Format("\"{0}\":\"{1}\"", name, value.ToString()));
        }
        public void AddItem(string name, string value)
        {
            _itemList.Add(string.Format("\"{0}\":\"{1}\"", name, value));
        }
        public void AddItem(string name, T value)
        {
            _itemList.Add(string.Format("\"{0}\":{1}", name, serializer.Serialize(value)));
        }
        public void AddItem(string name, List<T> value)
        {
            _itemList.Add(string.Format("\"{0}\":{1}", name, serializer.Serialize(value)));
        }
        public void AddItem(string name, JsonHelper value)
        {
            _itemList.Add(string.Format("\"{0}\":{1}", name, value));
        }
        public void AddItem(string name, List<JsonHelper> value)
        {
            _itemList.Add(string.Format("\"{0}\":[{1}]", name, string.Join(",", value)));
        }
        public override string ToString()
        {
            return "{" + string.Join(",", _itemList.ToArray()) + "}";
        }

    }
}