// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProfileValidationException.cs" company="">
//   
// </copyright>
// <summary>
//   The profile validation exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    /// <summary>
    /// The profile validation exception.
    /// </summary>
    [Serializable]
    public class ProfileValidationException : EntityValidationException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileValidationException"/> class.
        /// </summary>
        public ProfileValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ProfileValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public ProfileValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileValidationException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected ProfileValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}