using System.Threading.Tasks;

using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data
{
    public interface IUnitOfWork
    {
        #region Public Methods and Operators

        void Dispose();

        void Dispose(bool disposing);

        IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : Entity<TKey>;

        void Save();

        Task SaveAsync();

        #endregion
    }
}