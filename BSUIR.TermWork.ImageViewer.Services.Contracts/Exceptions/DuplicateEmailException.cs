// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DuplicateEmailException.cs" company="">
//   
// </copyright>
// <summary>
//   The duplicate email exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    /// <summary>
    /// The duplicate email exception.
    /// </summary>
    [Serializable]
    public class DuplicateEmailException : MembershipServiceException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEmailException"/> class.
        /// </summary>
        public DuplicateEmailException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEmailException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public DuplicateEmailException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEmailException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public DuplicateEmailException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEmailException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected DuplicateEmailException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}