using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBasicNHibernate.Vo
{
    public class Person : BaseVo<Int64>
    {
        public virtual String Name { get; set; }
        public virtual Int32 Age { get; set; }
    }
}