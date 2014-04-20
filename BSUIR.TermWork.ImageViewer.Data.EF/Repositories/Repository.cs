using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

using BSUIR.TermWork.ImageViewer.Data.Exceptions;
using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {
        #region Fields

        private readonly IDbContext _context;
        private readonly IDbSet<TEntity> _dbSet;

        #endregion

        #region Constructors and Destructors

        public Repository(IDbContext context)
        {
            this._context = context;
            this._dbSet = this.Context.Set<TEntity>();
        }

        #endregion

        #region Properties

        protected IDbContext Context
        {
            get { return this._context; }
        }

        protected IDbSet<TEntity> DbSet
        {
            get { return this._dbSet; }
        }

        #endregion

        #region Public Indexers

        public TEntity this[TKey key]
        {
            get
            {
                return this.FindByKey(key);
            }

            set
            {
                if (this.FindByKey(key) == null)
                {
                    this.Insert(value);
                }
                else
                {
                    this.Update(value);
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        public virtual void Delete(TKey key)
        {
            TEntity entity = this._dbSet.Find(key);
            this.Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            TEntity entityToDelete = this._dbSet.Find(entity.Key);
            try
            {
                if (entityToDelete != null)
                {
                    this._dbSet.Remove(entityToDelete);
                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex);
            }
        }

        public virtual TEntity FindByKey(TKey key)
        {
            return this._dbSet.Find(key);
        }

        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                this._dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                throw new DbException(ex);
            }
        }

        public virtual IRepositoryQuery<TEntity, TKey> Query()
        {
            IRepositoryQuery<TEntity, TKey> result = new RepositoryQuery<TEntity, TKey>(this);
            return result;
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this.ValidateEntity(entity);
            DbEntityEntry contextEntry = this._context.Entry(entity);
            if (contextEntry != null && contextEntry.State == EntityState.Detached)
            {
                TEntity oldEntity = this.Context.Set<TEntity>().Find(entity.Key);
                if (oldEntity != null)
                {
                    DbEntityEntry contextOldEntry = this._context.Entry(oldEntity);
                    contextOldEntry.CurrentValues.SetValues(entity);
                    contextOldEntry.State = EntityState.Modified;
                }
            }
        }

        #endregion

        #region Methods

        internal IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null)
        {
            IQueryable<TEntity> result = this._dbSet;
            if (includeProperties != null)
            {
                includeProperties.ForEach(prop => result.Include(prop));
            }
            if (filter != null)
            {
                result = result.Where(filter);
            }
            if (orderBy != null)
            {
                result = orderBy(result);
            }
            return result;
        }

        protected void ValidateEntity(object entity)
        {
            DbEntityEntry contextEntry = this._context.Entry(entity);
            if (contextEntry != null)
            {
                DbEntityValidationResult validationResult = contextEntry.GetValidationResult();
                if (!validationResult.IsValid)
                {
                    throw new DbException("The entity is not valid for current context!");
                }
            }
        }

        #endregion
    }
}