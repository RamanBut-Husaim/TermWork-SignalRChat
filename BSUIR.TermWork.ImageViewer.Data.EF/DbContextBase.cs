using System.Data.Entity;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF
{
    public class DbContextBase : DbContext, IDbContext
    {
        #region Constructors and Destructors

        public DbContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        #endregion

        #region Public Methods and Operators

        public new IDbSet<T> Set<T>() where T : EntityBase
        {
            return base.Set<T>();
        }

        #endregion
    }
}