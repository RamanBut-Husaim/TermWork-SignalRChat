using System;
using System.Runtime.Serialization;

using BSUIR.TermWork.ImageViewer.Shared.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Data.Exceptions
{
    [Serializable]
    public class DbException : ImageViewerException
    {
        #region Constants

        public const string DefaultMessage =
            "There are some problems with the database. See inner exception for more details";

        #endregion

        #region Constructors and Destructors

        public DbException()
        {
        }

        public DbException(string message = DefaultMessage)
            : base(message)
        {
        }

        public DbException(Exception innerException, string message = DefaultMessage)
            : base(message, innerException)
        {
        }

        protected DbException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}