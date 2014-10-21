using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBasicNHibernate.Vo
{
    /// <summary>
    /// Base Value Object class.
    /// </summary>
    /// <typeparam name="TIdentifier">Identifier Generic Type to be assigned dynamically.</typeparam>
    public class BaseVo<TIdentifier> 
        where TIdentifier : new()
    {
        /// <summary>
        /// Gets or sets the Identifier.
        /// </summary>
        public virtual TIdentifier Id { get; set; }
    }
}