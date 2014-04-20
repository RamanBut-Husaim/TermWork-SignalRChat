namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ImageAlbumServiceException : EntityValidationException
    {
        #region Constructors and Destructors

        public ImageAlbumServiceException()
        {
        }

        public ImageAlbumServiceException(string message = DefaultMessage)
            : base(message)
        {
        }

        public ImageAlbumServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ImageAlbumServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}