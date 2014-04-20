using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.Repositories
{
    public interface IRepository<TEntity, TKey> : IRepositoryBase
        where TEntity : Entity<TKey>
    {
        #region Public Indexers

        TEntity this[TKey key] { get; set; }

        #endregion

        #region Public Methods and Operators

        void Delete(TKey key);

        void Delete(TEntity entity);

        TEntity FindByKey(TKey key);

        void Insert(TEntity entity);

        IRepositoryQuery<TEntity, TKey> Query();

        void Update(TEntity entity);

        #endregion
    }
}