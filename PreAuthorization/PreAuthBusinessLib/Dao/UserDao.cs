using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using PreAuthBusinessLib.Fo;
using PreAuthEntityLib.Vo;
using Ralph.CommonLib.Dao;
using Ralph.CommonLib.NHibernate;

namespace PreAuthBusinessLib.Dao
{
    public class UserDao : BaseDao<UserVo, Int64>
    {
        public UserDao(SessionHelper sessionHelper)
        {
            base.CurrentSession = sessionHelper.Current;
        }
        public IList<UserVo> QueryLogin(UserFo fo)
        {
            var crt = CurrentSession.CreateCriteria(typeof(UserVo));

            var criteria = crt.Add(Restrictions.Eq("UserCode", fo.UserCode))
                .Add(Restrictions.Eq("AgencyCode",fo.AgencyCode));

            return criteria.List<UserVo>();
        }

        public override IList<UserVo> LoadPage(int pageIndex, int pageSize, ref int totalCount)
        {
            var list = base.LoadAll();
            totalCount = list.Count;
            var v = (from p in list
                     where p.Id >0
                     orderby p.Id descending
                     select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return v.ToList();
        }
    }
}
