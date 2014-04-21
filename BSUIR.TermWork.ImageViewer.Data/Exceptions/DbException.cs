// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbException.cs" company="">
//   
// </copyright>
// <summary>
//   The db exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

using BSUIR.TermWork.ImageViewer.Shared.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Data.Exceptions
{
    /// <summary>
    /// The db exception.
    /// </summary>
    [Serializable]
    public class DbException : ImageViewerException
    {
        #region Constants

        /// <summary>
        /// The default message.
        /// </summary>
        public const string DefaultMessage =
            "There are some problems with the database. See inner exception for more details";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DbException"/> class.
        /// </summary>
        public DbException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public DbException(string message = DefaultMessage) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbException"/> class.
        /// </summary>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public DbException(Exception innerException, string message = DefaultMessage)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected DbException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}