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
    public class AuthorityDao : BaseDao<AuthorityVo, Int64>
    {
        public AuthorityDao(SessionHelper sessionHelper)
        {
            base.CurrentSession = sessionHelper.Current;
        }
        public IList<AuthorityVo> QueryByCondiftion(AuthorityFo fo)
        {
            var crt = CurrentSession.CreateCriteria(typeof(AuthorityVo));

            var criteria = crt.Add(Restrictions.Eq("RoleCode", fo.RoleCode));

            return criteria.List<AuthorityVo>();
        }
    }
}
