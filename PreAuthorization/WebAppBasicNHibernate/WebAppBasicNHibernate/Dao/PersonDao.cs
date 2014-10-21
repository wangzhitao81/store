using System;
using NHibernate.Util;
using WebAppBasicNHibernate.Vo;
using System.Collections.Generic;
using NHibernate.Linq;

namespace WebAppBasicNHibernate.Dao
{
    public class PersonDao : BaseDao<Person, Int64>
    {
        public void DeleteByName(String name)
        {
            var queryResult = CurrentSession.QueryOver<Person>()
                              .Where(p => p.Name == name);

            if (queryResult != null && queryResult.RowCount() > 0)
            {
                IList<Person> peopleToBeDeleted = queryResult.List();
                peopleToBeDeleted.ForEach(personToBeDeleted => CurrentSession.Delete(personToBeDeleted));
                CurrentSession.Flush();
            }
        }
    }
}