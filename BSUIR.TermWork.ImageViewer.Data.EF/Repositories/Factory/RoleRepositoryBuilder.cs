// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleRepositoryBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The role repository builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The role repository builder.
    /// </summary>
    public sealed class RoleRepositoryBuilder : IRepositoryBuilder<Role, int>
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
        public IRepository<Role, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<Role, int>(unitOfWork.Context);
        }

        #endregion
    }
}