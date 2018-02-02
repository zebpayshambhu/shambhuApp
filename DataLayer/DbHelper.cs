using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UtilityLayer;
using EntityLayer;

namespace DataLayer
{
    public class DbHelper<TEntity> where TEntity : class
    {
        internal DbEntities _dbContext;
        internal DbSet<TEntity> _dbSet;

        public DbHelper(DbEntities dbcontext)
        {
            this._dbContext = dbcontext;
            this._dbContext.Configuration.ValidateOnSaveEnabled = false;
            this._dbSet = dbcontext.Set<TEntity>();
        }

        /// <summary>
        /// Generic method to check if entity exists
        /// </summary>
        /// <param name="pkId">Primary Key of table</param>
        /// <returns>Boolean return i.e. either TRUE or FALSE</returns>
        public bool Exists(object pkId)
        {
            return _dbSet.Find(pkId) != null;
        }

        /// <summary>
        /// Extract first record based on given condition. Returns null if no record found.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public TEntity GetFirst(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, Int64>> order = null)
        {
            IQueryable<TEntity> lstResult = _dbSet.AsQueryable();
            if (order != null)
                lstResult = lstResult.OrderBy(order).AsQueryable();
            if (condition != null)
                lstResult = lstResult.Where(condition).AsQueryable();

            TEntity entity = lstResult.FirstOrDefault();
            return entity;
        }

        /// <summary>
        /// Extract multiple records.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, string>> order = null)
        {
            IQueryable<TEntity> lstResult = _dbSet.AsQueryable();
            if (condition != null)
                lstResult = lstResult.Where(condition).AsQueryable();
            if (order != null)
                lstResult = lstResult.OrderBy(order).AsQueryable();
            return lstResult;
        }

        /// <summary>
        /// Extract multiple records, use this method to incorporate pagination functionality.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="_order"></param>
        /// <param name="currentPageIndex"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetPagedList(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, Int64>> _order, int currentPageIndex, out int Total)
        {
            IQueryable<TEntity> lstResult = _dbSet.AsQueryable();
            if (condition != null)
                lstResult = lstResult.Where(condition).AsQueryable();
            Total = lstResult.Count();
            lstResult = lstResult.OrderByDescending(_order).AsQueryable().Skip((currentPageIndex - 1) * StringUtility.ItemsPerPage).Take(StringUtility.ItemsPerPage);
            return lstResult;
        }

        /// <summary>
        /// Insert single record
        /// </summary>
        /// <param name="entity"></param>
        public virtual void InsertEntity(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Insert multiple records
        /// </summary>
        /// <param name="entity"></param>
        public virtual void InsertEntity(List<TEntity> entity)
        {
            _dbSet.AddRange(entity);
        }

        /// <summary>
        /// Update single entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="NoUpdateProperty"></param>
        public virtual void UpdateEntity(TEntity entity, string[] NoUpdateProperty = null)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry<TEntity>(entity).State = EntityState.Modified;
            if (NoUpdateProperty != null)
            {
                foreach (string item in NoUpdateProperty)
                {
                    _dbContext.Entry<TEntity>(entity).Property(item).IsModified = false;
                }
            }
        }

        /// <summary>
        /// Update multiple entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="NoUpdateProperty"></param>
        public virtual void UpdateEntity(List<TEntity> entity, string[] NoUpdateProperty = null)
        {
            foreach (TEntity item in entity)
            {
                _dbSet.Attach(item);
                _dbContext.Entry<TEntity>(item).State = EntityState.Modified;
                if (NoUpdateProperty != null)
                {
                    foreach (string item1 in NoUpdateProperty)
                    {
                        _dbContext.Entry<TEntity>(item).Property(item1).IsModified = false;
                    }
                }
            }
        }

        /// <summary>
        /// Generic Delete method for the entities. Delete one record only.
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Generic Delete method for the entities. Delete one record only.
        /// </summary>
        /// <param name="entityToDelete"></param>
        public virtual void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Delete one or many records based on given condition
        /// </summary>
        /// <param name="condition"></param>
        public void DeleteEntity(Expression<Func<TEntity, bool>> condition)
        {
            _dbSet.RemoveRange(_dbSet.Where(condition));
        }
    }
}
