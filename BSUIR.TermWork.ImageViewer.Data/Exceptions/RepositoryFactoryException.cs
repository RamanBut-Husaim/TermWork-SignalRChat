// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryFactoryException.cs" company="">
//   
// </copyright>
// <summary>
//   The repository factory exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

using BSUIR.TermWork.ImageViewer.Shared.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Data.Exceptions
{
    /// <summary>
    /// The repository factory exception.
    /// </summary>
    [Serializable]
    public class RepositoryFactoryException : ImageViewerException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFactoryException"/> class.
        /// </summary>
        public RepositoryFactoryException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFactoryException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public RepositoryFactoryException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFactoryException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public RepositoryFactoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFactoryException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected RepositoryFactoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}