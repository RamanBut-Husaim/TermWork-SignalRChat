// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FriendshipRepositoryBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The friendship repository builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The friendship repository builder.
    /// </summary>
    internal sealed class FriendshipRepositoryBuilder : IRepositoryBuilder<Friendship, int>
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
        public IRepository<Friendship, int> Build(UnitOfWork unitOfWork)
        {
            return new FriendshipRepository(
                unitOfWork.Context, 
                unitOfWork.Repository<Message, int>());
        }

        #endregion
    }
}