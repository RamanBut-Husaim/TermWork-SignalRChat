using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.Repositories
{
    public interface IRepositoryQuery<TEntity, TKey>
        where TEntity : Entity<TKey>
    {
        #region Public Methods and Operators

        IRepositoryQuery<TEntity, TKey> Filter(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> Get();

        IRepositoryQuery<TEntity, TKey> Include(Expression<Func<TEntity, object>> expression);

        IRepositoryQuery<TEntity, TKey> OrderBy(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

        #endregion
    }
}