using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using NHibernate;
using NHibernate.Linq;
using Ralph.CommonLib.NHibernate;
using Ralph.CommonLib.Vo;

namespace Ralph.CommonLib.Dao
{
    public class BaseDao<TEntity, TIdentifier>
        where TIdentifier : new()
        where TEntity : BaseVo<TIdentifier> 
    {
        /// <summary>
        /// NHibernate ISession to be used to manipulate data in the
        /// database.
        /// </summary>
        protected ISession CurrentSession { get; set; }
        public BaseDao()
        {
            SessionHelper sessionHelper = new SessionHelper();
            CurrentSession = sessionHelper.Current;
        }
        public BaseDao(SessionHelper sessionHelper)
        {
            //SessionHelper sessionHelper = new SessionHelper();
            CurrentSession = sessionHelper.Current;
        }

        /// <summary>
        /// Load an Entity by its identifier.
        /// </summary>
        /// <param name="id">Entity´s identifier.</param>
        /// <returns>Entity Object.</returns>
        public TEntity LoadById(TIdentifier id)
        {
            TEntity entity = CurrentSession.Get<TEntity>(id);
            return entity;
        }

        /// <summary>
        /// Create an Entity.
        /// </summary>
        /// <param name="entity">Entity to be created.</param>
        /// <returns>Identfifier of the created Entity.</returns>
        public TIdentifier Create(TEntity entity)
        {
            TIdentifier identifier = new TIdentifier();
            //using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required))
            {
                identifier = (TIdentifier)CurrentSession.Save(entity);
                //transaction.Complete();
            }
            return identifier;
        }
        public TIdentifier CreateIndTrans(TEntity entity)
        {
            TIdentifier identifier = new TIdentifier();
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required))
            {
                identifier = (TIdentifier)CurrentSession.Save(entity);
                transaction.Complete();
            }
            return identifier;
        }
        /// <summary>
        /// Save or Update an Entity.
        /// </summary>
        /// <param name="entity">Entity to be saved or updated.</param>
        public void SaveOrUpdate(TEntity entity)
        {
            TIdentifier identifier = new TIdentifier();
            //using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required))
            {
                CurrentSession.SaveOrUpdate(entity);
                //transaction.Complete();
            }
        }
        public void SaveOrUpdateIndTrans(TEntity entity)
        {
            TIdentifier identifier = new TIdentifier();
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required))
            {
                CurrentSession.SaveOrUpdate(entity);
                transaction.Complete();
            }
        }
        /// <summary>
        /// Update an existing Enitty.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        public void Update(TEntity entity)
        {
            //using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required))
            {
                CurrentSession.Update(entity);
                CurrentSession.Flush();
                //transaction.Complete();
            }
        }
        public void UpdateIndTrans(TEntity entity)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required))
            {
                CurrentSession.Update(entity);
                CurrentSession.Flush();
                transaction.Complete();
            }
        }
        /// <summary>
        /// Delete an Entity based on its Instance.
        /// </summary>
        /// <param name="entity">Entity Instance.</param>
        public void Delete(TEntity entity)
        {
            //using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required))
            {
                CurrentSession.Delete(entity);
                //transaction.Complete();
            }
        }
        public void DeleteIndTrans(TEntity entity)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required))
            {
                CurrentSession.Delete(entity);
                transaction.Complete();
            }
        }
        /// <summary>
        /// Delete an Entity based on its Identifier.
        /// </summary>
        /// <param name="entityIdentifier">Entity Identifier.</param>
        public void DeleteById(TIdentifier entityIdentifier)
        {
            //using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required))
            {
                TEntity entity = LoadById(entityIdentifier);
                CurrentSession.Delete(entity);
                //transaction.Complete();
            }
        }
        public void DeleteByIdIndTrans(TIdentifier entityIdentifier)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required))
            {
                TEntity entity = LoadById(entityIdentifier);
                CurrentSession.Delete(entity);
                transaction.Complete();
            }
        }
        /// <summary>
        /// Retrieve all Entities from the database.
        /// </summary>
        /// <returns>List of all entities.</returns>
        public virtual IList<TEntity> LoadAll()
        {
            return CurrentSession.Query<TEntity>().ToList();
        }
        public virtual IList<TEntity> LoadPage(int pageIndex, int pageSize, ref int totalCount)
        {
            var list = LoadAll();
            totalCount = list.Count;
            var v = (from p in list
                     orderby p.Id descending
                     select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return v.ToList();
        }
    }
}
