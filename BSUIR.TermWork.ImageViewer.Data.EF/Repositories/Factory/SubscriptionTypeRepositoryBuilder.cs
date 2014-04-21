// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriptionTypeRepositoryBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The subscription type repository builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The subscription type repository builder.
    /// </summary>
    internal sealed class SubscriptionTypeRepositoryBuilder :
        IRepositoryBuilder<SubscriptionType, int>
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
        public IRepository<SubscriptionType, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<SubscriptionType, int>(unitOfWork.Context);
        }

        #endregion
    }
}