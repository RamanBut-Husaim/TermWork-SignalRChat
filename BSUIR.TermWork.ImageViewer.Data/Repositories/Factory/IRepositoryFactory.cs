// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepositoryFactory.cs" company="">
//   
// </copyright>
// <summary>
//   The RepositoryFactory interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.Repositories.Factory
{
    /// <summary>
    /// The RepositoryFactory interface.
    /// </summary>
    public interface IRepositoryFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// The build repository.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <typeparam name="TKey">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IRepository"/>.
        /// </returns>
        IRepository<TEntity, TKey> BuildRepository<TEntity, TKey>(IUnitOfWork unitOfWork)
            where TEntity : Entity<TKey>;

        #endregion
    }
}