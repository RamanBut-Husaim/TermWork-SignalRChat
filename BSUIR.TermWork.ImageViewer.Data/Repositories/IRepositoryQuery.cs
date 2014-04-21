// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepositoryQuery.cs" company="">
//   
// </copyright>
// <summary>
//   The RepositoryQuery interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.Repositories
{
    /// <summary>
    /// The RepositoryQuery interface.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    /// <typeparam name="TKey">
    /// </typeparam>
    public interface IRepositoryQuery<TEntity, TKey> where TEntity : Entity<TKey>
    {
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
        IRepositoryQuery<TEntity, TKey> Filter(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<TEntity> Get();

        /// <summary>
        /// The include.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <returns>
        /// The <see cref="IRepositoryQuery"/>.
        /// </returns>
        IRepositoryQuery<TEntity, TKey> Include(Expression<Func<TEntity, object>> expression);

        /// <summary>
        /// The order by.
        /// </summary>
        /// <param name="orderBy">
        /// The order by.
        /// </param>
        /// <returns>
        /// The <see cref="IRepositoryQuery"/>.
        /// </returns>
        IRepositoryQuery<TEntity, TKey> OrderBy(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

        #endregion
    }
}