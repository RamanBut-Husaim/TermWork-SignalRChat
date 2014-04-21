// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommentServiceException.cs" company="">
//   
// </copyright>
// <summary>
//   The comment service exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    /// <summary>
    /// The comment service exception.
    /// </summary>
    [Serializable]
    public class CommentServiceException : EntityValidationException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentServiceException"/> class.
        /// </summary>
        public CommentServiceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentServiceException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public CommentServiceException(string message = DefaultMessage) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentServiceException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public CommentServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentServiceException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected CommentServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}