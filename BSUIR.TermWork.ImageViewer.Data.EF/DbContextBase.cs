// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbContextBase.cs" company="">
//   
// </copyright>
// <summary>
//   The db context base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Data.Entity;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF
{
    /// <summary>
    /// The db context base.
    /// </summary>
    public class DbContextBase : DbContext, IDbContext
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextBase"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">
        /// The name or connection string.
        /// </param>
        public DbContextBase(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The set.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IDbSet"/>.
        /// </returns>
        public new IDbSet<T> Set<T>() where T : EntityBase
        {
            return base.Set<T>();
        }

        #endregion
    }
}