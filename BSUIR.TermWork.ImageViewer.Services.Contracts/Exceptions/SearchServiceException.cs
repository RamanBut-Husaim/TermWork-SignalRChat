// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchServiceException.cs" company="">
//   
// </copyright>
// <summary>
//   The search service exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    /// <summary>
    /// The search service exception.
    /// </summary>
    [Serializable]
    public class SearchServiceException : EntityValidationException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchServiceException"/> class.
        /// </summary>
        public SearchServiceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchServiceException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public SearchServiceException(string message = DefaultMessage) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchServiceException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public SearchServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchServiceException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected SearchServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}