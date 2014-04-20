using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data
{
    public interface IDbContext
    {
        #region Public Methods and Operators

        void Dispose();

        DbEntityEntry Entry(object o);

        int SaveChanges();

        Task<int> SaveChangesAsync();

        IDbSet<T> Set<T>() where T : EntityBase;

        #endregion
    }
}