using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.Repositories.Factory
{
    public interface IRepositoryFactory
    {
        #region Public Methods and Operators

        IRepository<TEntity, TKey> BuildRepository<TEntity, TKey>(IUnitOfWork unitOfWork)
            where TEntity : Entity<TKey>;

        #endregion
    }
}