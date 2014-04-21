// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccessRightValidationException.cs" company="">
//   
// </copyright>
// <summary>
//   The access right validation exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    /// <summary>
    /// The access right validation exception.
    /// </summary>
    [Serializable]
    public class AccessRightValidationException : EntityValidationException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessRightValidationException"/> class.
        /// </summary>
        public AccessRightValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessRightValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public AccessRightValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessRightValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public AccessRightValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessRightValidationException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected AccessRightValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}