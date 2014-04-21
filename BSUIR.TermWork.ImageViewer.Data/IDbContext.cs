// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDbContext.cs" company="">
//   
// </copyright>
// <summary>
//   The DbContext interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data
{
    /// <summary>
    /// The DbContext interface.
    /// </summary>
    public interface IDbContext
    {
        #region Public Methods and Operators

        /// <summary>
        /// The dispose.
        /// </summary>
        void Dispose();

        /// <summary>
        /// The entry.
        /// </summary>
        /// <param name="o">
        /// The o.
        /// </param>
        /// <returns>
        /// The <see cref="DbEntityEntry"/>.
        /// </returns>
        DbEntityEntry Entry(object o);

        /// <summary>
        /// The save changes.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int SaveChanges();

        /// <summary>
        /// The save changes async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// The set.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IDbSet"/>.
        /// </returns>
        IDbSet<T> Set<T>() where T : EntityBase;

        #endregion
    }
}