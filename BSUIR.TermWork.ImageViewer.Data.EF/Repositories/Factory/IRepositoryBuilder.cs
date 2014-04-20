using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    internal interface IRepositoryBuilder<TEntity, TKey>
        where TEntity : Entity<TKey>
    {
        IRepository<TEntity, TKey> Build(UnitOfWork unitOfWork);
    }
}
