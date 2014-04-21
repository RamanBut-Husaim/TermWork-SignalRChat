// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryQuery.cs" company="">
//   
// </copyright>
// <summary>
//   The repository query.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories
{
    /// <summary>
    /// The repository query.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    /// <typeparam name="TKey">
    /// </typeparam>
    public sealed class RepositoryQuery<TEntity, TKey> : IRepositoryQuery<TEntity, TKey>
        where TEntity : Entity<TKey>
    {
        #region Fields

        /// <summary>
        /// The _included properies.
        /// </summary>
        private readonly List<Expression<Func<TEntity, object>>> _includedProperies;

        /// <summary>
        /// The _repository.
        /// </summary>
        private readonly IRepository<TEntity, TKey> _repository;

        /// <summary>
        /// The _filter.
        /// </summary>
        private Expression<Func<TEntity, bool>> _filter;

        /// <summary>
        /// The _ordered by querable.
        /// </summary>
        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderedByQuerable;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryQuery{TEntity,TKey}"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public RepositoryQuery(IRepository<TEntity, TKey> repository)
        {
            this._repository = repository;
            this._includedProperies = new List<Expression<Func<TEntity, object>>>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The filter.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="IRepositoryQuery"/>.
        /// </returns>
        public IRepositoryQuery<TEntity, TKey> Filter(Expression<Func<TEntity, bool>> filter)
        {
            this._filter = filter;
            return this;
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<TEntity> Get()
        {
            return ((Repository<TEntity, TKey>)this._repository).Get(
                this._filter, 
                this._orderedByQuerable, 
                this._includedProperies);
        }

        /// <summary>
        /// The include.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <returns>
        /// The <see cref="IRepositoryQuery"/>.
        /// </returns>
        public IRepositoryQuery<TEntity, TKey> Include(Expression<Func<TEntity, object>> expression)
        {
            this._includedProperies.Add(expression);
            return this;
        }

        /// <summary>
        /// The order by.
        /// </summary>
        /// <param name="orderBy">
        /// The order by.
        /// </param>
        /// <returns>
        /// The <see cref="IRepositoryQuery"/>.
        /// </returns>
        public IRepositoryQuery<TEntity, TKey> OrderBy(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            this._orderedByQuerable = orderBy;
            return this;
        }

        #endregion
    }
}