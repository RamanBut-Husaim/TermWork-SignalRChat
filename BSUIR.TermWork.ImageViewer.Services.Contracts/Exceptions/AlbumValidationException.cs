// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlbumValidationException.cs" company="">
//   
// </copyright>
// <summary>
//   The album validation exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    /// <summary>
    /// The album validation exception.
    /// </summary>
    [Serializable]
    public class AlbumValidationException : EntityValidationException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumValidationException"/> class.
        /// </summary>
        public AlbumValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public AlbumValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public AlbumValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumValidationException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected AlbumValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}