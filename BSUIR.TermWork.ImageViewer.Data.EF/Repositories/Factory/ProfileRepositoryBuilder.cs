// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProfileRepositoryBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The profile repository builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The profile repository builder.
    /// </summary>
    internal sealed class ProfileRepositoryBuilder : IRepositoryBuilder<Profile, int>
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
        public IRepository<Profile, int> Build(UnitOfWork unitOfWork)
        {
            return new ProfileRepository(
                unitOfWork.Context, 
                unitOfWork.Repository<Subscription, int>(), 
                unitOfWork.Repository<Friendship, int>() as IFriendshipRepository);
        }

        #endregion
    }
}