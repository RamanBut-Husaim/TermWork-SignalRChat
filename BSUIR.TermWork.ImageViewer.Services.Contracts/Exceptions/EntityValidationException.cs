// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityValidationException.cs" company="">
//   
// </copyright>
// <summary>
//   The entity validation exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

using BSUIR.TermWork.ImageViewer.Shared.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    /// <summary>
    /// The entity validation exception.
    /// </summary>
    [Serializable]
    public class EntityValidationException : ImageViewerException
    {
        #region Constants

        /// <summary>
        /// The default message.
        /// </summary>
        public const string DefaultMessage =
            "The operation cannot be completed. See inner exception for more details.";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityValidationException"/> class.
        /// </summary>
        public EntityValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public EntityValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public EntityValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityValidationException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected EntityValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}