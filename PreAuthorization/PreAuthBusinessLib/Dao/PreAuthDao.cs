using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Util;
using PreAuthBusinessLib.Fo;
using PreAuthEntityLib.Vo;
using Ralph.CommonLib.Dao;
using Ralph.CommonLib.NHibernate;

namespace PreAuthBusinessLib.Dao
{
    public class PreAuthDao : BaseDao<PreAuthVo, Int64>
    {
        public PreAuthDao()
        {
        }

        public PreAuthDao(SessionHelper sessionHelper)
        {
            base.CurrentSession = sessionHelper.Current;
        }
        public IList<PreAuthVo> QueryByCondition(PreAuthFo fo, int pageIndex, int pageSize, ref int totalCount)
        {
            IList<PreAuthVo> rList=new List<PreAuthVo>();
            if (!string.IsNullOrEmpty(fo.TelNumber))
            {
                var telNum = fo.TelNumber;
                rList = CurrentSession.QueryOver<PreAuthVo>()
                    .Where(p => p.TelNumber == telNum).List();
            }
            if (string.IsNullOrEmpty((fo.CustomerIdNo))) return rList;
            var idNo = fo.CustomerIdNo;
            if (string.IsNullOrEmpty(fo.TelNumber))
            {
                rList = CurrentSession.QueryOver<PreAuthVo>()
                    .Where(p => p.TelNumber == idNo).List();
            }
            else
            {
                rList = (from p in rList
                    where p.CustomerIdNo == idNo
                    select p).ToList();
            }
            return rList;
        }
    }
}
