// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageContentRepositoryBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The image content repository builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The image content repository builder.
    /// </summary>
    internal sealed class ImageContentRepositoryBuilder : IRepositoryBuilder<ImageContent, int>
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
        public IRepository<ImageContent, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<ImageContent, int>(unitOfWork.Context);
        }

        #endregion
    }
}