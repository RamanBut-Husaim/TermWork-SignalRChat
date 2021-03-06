﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TagRepositoryBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The tag repository builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The tag repository builder.
    /// </summary>
    internal sealed class TagRepositoryBuilder : IRepositoryBuilder<Tag, int>
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
        public IRepository<Tag, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<Tag, int>(unitOfWork.Context);
        }

        #endregion
    }
}