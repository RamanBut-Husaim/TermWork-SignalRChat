// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriptionServiceException.cs" company="">
//   
// </copyright>
// <summary>
//   The subscription service exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    /// <summary>
    /// The subscription service exception.
    /// </summary>
    [Serializable]
    public class SubscriptionServiceException : EntityValidationException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionServiceException"/> class.
        /// </summary>
        public SubscriptionServiceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionServiceException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public SubscriptionServiceException(string message = DefaultMessage) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionServiceException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public SubscriptionServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionServiceException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected SubscriptionServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}