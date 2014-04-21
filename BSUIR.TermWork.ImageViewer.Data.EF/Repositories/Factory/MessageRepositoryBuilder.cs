﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageRepositoryBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The message repository builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The message repository builder.
    /// </summary>
    internal sealed class MessageRepositoryBuilder : IRepositoryBuilder<Message, int>
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
        public IRepository<Message, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<Message, int>(unitOfWork.Context);
        }

        #endregion
    }
}