// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepositoryBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The RepositoryBuilder interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The RepositoryBuilder interface.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    /// <typeparam name="TKey">
    /// </typeparam>
    internal interface IRepositoryBuilder<TEntity, TKey> where TEntity : Entity<TKey>
    {
        #region Public Methods and Operators

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        /// <returns>
        /// The <see cref="IRepository"/>.
        /// </returns>
        IRepository<TEntity, TKey> Build(UnitOfWork unitOfWork);

        #endregion
    }
}