// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageValidationException.cs" company="">
//   
// </copyright>
// <summary>
//   The image validation exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    /// <summary>
    /// The image validation exception.
    /// </summary>
    [Serializable]
    public class ImageValidationException : EntityValidationException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageValidationException"/> class.
        /// </summary>
        public ImageValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ImageValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public ImageValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageValidationException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected ImageValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}