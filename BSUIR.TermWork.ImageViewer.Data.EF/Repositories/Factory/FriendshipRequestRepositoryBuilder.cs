// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FriendshipRequestRepositoryBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The friendship request repository builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The friendship request repository builder.
    /// </summary>
    internal sealed class FriendshipRequestRepositoryBuilder :
        IRepositoryBuilder<FriendshipRequest, int>
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
        public IRepository<FriendshipRequest, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<FriendshipRequest, int>(unitOfWork.Context);
        }

        #endregion
    }
}