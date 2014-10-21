using PreAuthEntityLib.Vo;
using Ralph.CommonLib.Dao;
using Ralph.CommonLib.NHibernate;
using Ralph.CommonLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreAuthMvc.Common
{
    public class PreAuthUtil
    {
        private readonly SessionHelper sessionHelper;
        public PreAuthUtil(SessionHelper helper)
        {
            this.sessionHelper = helper;
        }
        public static void SetPageInfo(ViewDataDictionary viewData, int pageNo, int totalCount)
        {
            viewData["pageNo"] = pageNo;
            viewData["totalRecords"] = totalCount;
            viewData["totalPages"] = CommonUtil.CountTotalPages(totalCount, ConstValues.PageSize);
        }

        public IList<SelectListItem> GetFunctionSelectListItems()
        {
            var dao = new BaseDao<FunctionVo, long>(sessionHelper);
            IList<SelectListItem> agencyList = (from p in dao.LoadAll()
                                                select new SelectListItem
                                                {
                                                    Text = p.FunctionName,
                                                    Value = p.FunctionCode
                                                }).ToList();
            return agencyList;
        }
        public IList<SelectListItem> GetRoleSelectListItems()
        {
            var dao = new BaseDao<RoleVo, long>(sessionHelper);
            IList<SelectListItem> roleList = (from p in dao.LoadAll()
                                              select new SelectListItem
                                              {
                                                  Text = p.RoleName,
                                                  Value = p.RoleCode
                                              }).ToList();
            return roleList;
        }
        public IList<SelectListItem> GetAgencySelectListItems()
        {
            var dao = new BaseDao<AgencyVo, long>(sessionHelper);
            IList<SelectListItem> agencyList = (from p in dao.LoadAll()
                                                select new SelectListItem
                                                {
                                                    Text = p.AgencyName,
                                                    Value = p.AgencyCode
                                                }).ToList();
            return agencyList;
        }
    }
}