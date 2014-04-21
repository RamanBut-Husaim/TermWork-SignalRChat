// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageRepositoryBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The image repository builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The image repository builder.
    /// </summary>
    internal sealed class ImageRepositoryBuilder : IRepositoryBuilder<Image, int>
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
        public IRepository<Image, int> Build(UnitOfWork unitOfWork)
        {
            return new ImageRepository(
                unitOfWork.Context, 
                unitOfWork.Repository<ImageContent, int>(), 
                unitOfWork.Repository<Comment, int>());
        }

        #endregion
    }
}