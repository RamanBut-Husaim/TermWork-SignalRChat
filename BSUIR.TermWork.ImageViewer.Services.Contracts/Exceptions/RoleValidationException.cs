namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class RoleValidationException : EntityValidationException
    {
        #region Constructors and Destructors

        public RoleValidationException()
        {
        }

        public RoleValidationException(string message)
            : base(message)
        {
        }

        public RoleValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected RoleValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}