// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepositoryBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The user repository builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The user repository builder.
    /// </summary>
    internal sealed class UserRepositoryBuilder : IRepositoryBuilder<User, int>
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
        public IRepository<User, int> Build(UnitOfWork unitOfWork)
        {
            return new UserRepository(
                unitOfWork.Context, 
                unitOfWork.Repository<Profile, int>() as IProfileRepository, 
                unitOfWork.Repository<Album, int>() as IAlbumRepository);
        }

        #endregion
    }
}