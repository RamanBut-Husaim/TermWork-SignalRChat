// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommentRepositoryBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The comment repository builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The comment repository builder.
    /// </summary>
    internal sealed class CommentRepositoryBuilder : IRepositoryBuilder<Comment, int>
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
        public IRepository<Comment, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<Comment, int>(unitOfWork.Context);
        }

        #endregion
    }
}