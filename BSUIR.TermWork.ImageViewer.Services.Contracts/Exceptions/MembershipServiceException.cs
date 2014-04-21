// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MembershipServiceException.cs" company="">
//   
// </copyright>
// <summary>
//   The membership service exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    /// <summary>
    /// The membership service exception.
    /// </summary>
    [Serializable]
    public class MembershipServiceException : EntityValidationException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipServiceException"/> class.
        /// </summary>
        public MembershipServiceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipServiceException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public MembershipServiceException(string message = DefaultMessage) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipServiceException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public MembershipServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipServiceException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected MembershipServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}