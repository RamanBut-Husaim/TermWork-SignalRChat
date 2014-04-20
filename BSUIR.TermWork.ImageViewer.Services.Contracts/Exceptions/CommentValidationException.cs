namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class CommentValidationException : EntityValidationException
    {
        #region Constructors and Destructors

        public CommentValidationException()
        {
        }

        public CommentValidationException(string message)
            : base(message)
        {
        }

        public CommentValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CommentValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}