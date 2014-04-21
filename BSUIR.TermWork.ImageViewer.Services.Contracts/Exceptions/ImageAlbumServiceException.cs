// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageAlbumServiceException.cs" company="">
//   
// </copyright>
// <summary>
//   The image album service exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    /// <summary>
    /// The image album service exception.
    /// </summary>
    [Serializable]
    public class ImageAlbumServiceException : EntityValidationException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAlbumServiceException"/> class.
        /// </summary>
        public ImageAlbumServiceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAlbumServiceException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ImageAlbumServiceException(string message = DefaultMessage) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAlbumServiceException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public ImageAlbumServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAlbumServiceException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected ImageAlbumServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}