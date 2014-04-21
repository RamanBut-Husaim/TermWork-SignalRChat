// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccessRightRepositoryBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The access right repository builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The access right repository builder.
    /// </summary>
    internal sealed class AccessRightRepositoryBuilder : IRepositoryBuilder<AccessRight, int>
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
        public IRepository<AccessRight, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<AccessRight, int>(unitOfWork.Context);
        }

        #endregion
    }
}