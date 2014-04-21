// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserValidationException.cs" company="">
//   
// </copyright>
// <summary>
//   The user validation exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    /// <summary>
    /// The user validation exception.
    /// </summary>
    [Serializable]
    public class UserValidationException : EntityValidationException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidationException"/> class.
        /// </summary>
        public UserValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public UserValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public UserValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidationException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected UserValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}