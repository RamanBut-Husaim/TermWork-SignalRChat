using System;
using System.Runtime.Serialization;

using BSUIR.TermWork.ImageViewer.Shared.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Data.Exceptions
{
    [Serializable]
    public class RepositoryFactoryException : ImageViewerException
    {
        #region Constructors and Destructors

        public RepositoryFactoryException()
        {
        }

        public RepositoryFactoryException(string message)
            : base(message)
        {
        }

        public RepositoryFactoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected RepositoryFactoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}