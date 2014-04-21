// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlbumRepositoryBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The album repository builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The album repository builder.
    /// </summary>
    internal sealed class AlbumRepositoryBuilder : IRepositoryBuilder<Album, int>
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
        public IRepository<Album, int> Build(UnitOfWork unitOfWork)
        {
            return new AlbumRepository(
                unitOfWork.Context, 
                unitOfWork.Repository<Image, int>() as IImageRepository);
        }

        #endregion
    }
}