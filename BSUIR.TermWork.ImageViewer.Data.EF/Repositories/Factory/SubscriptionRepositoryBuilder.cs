// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriptionRepositoryBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The subscription repository builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The subscription repository builder.
    /// </summary>
    internal sealed class SubscriptionRepositoryBuilder : IRepositoryBuilder<Subscription, int>
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
        public IRepository<Subscription, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<Subscription, int>(unitOfWork.Context);
        }

        #endregion
    }
}