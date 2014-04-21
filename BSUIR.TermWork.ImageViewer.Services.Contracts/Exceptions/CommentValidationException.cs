// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommentValidationException.cs" company="">
//   
// </copyright>
// <summary>
//   The comment validation exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    /// <summary>
    /// The comment validation exception.
    /// </summary>
    [Serializable]
    public class CommentValidationException : EntityValidationException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentValidationException"/> class.
        /// </summary>
        public CommentValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public CommentValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public CommentValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentValidationException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected CommentValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}