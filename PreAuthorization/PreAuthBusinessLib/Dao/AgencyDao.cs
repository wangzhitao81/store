using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using PreAuthBusinessLib.Fo;
using PreAuthEntityLib.Vo;
using Ralph.CommonLib.Dao;
using Ralph.CommonLib.NHibernate;

namespace PreAuthBusinessLib.Dao
{
    public class AgencyDao : BaseDao<UserVo, Int64>
    {
        public AgencyDao(SessionHelper sessionHelper)
        {
            base.CurrentSession = sessionHelper.Current;
        }
        public IList<AgencyVo> QueryByCondition(AgencyFo fo)
        {
            var crt = CurrentSession.CreateCriteria(typeof(AgencyVo));

            var criteria = crt.Add(Restrictions.Eq("AgencyCode", fo.AgencyCode));

            return criteria.List<AgencyVo>();
        }
    }
}
