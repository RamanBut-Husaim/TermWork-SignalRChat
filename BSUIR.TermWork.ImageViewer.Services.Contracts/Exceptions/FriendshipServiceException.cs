using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    [Serializable]
    public sealed class FriendshipServiceException : EntityValidationException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipServiceException"/> class.
        /// </summary>
        public FriendshipServiceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipServiceException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public FriendshipServiceException(string message = DefaultMessage) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipServiceException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public FriendshipServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipServiceException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected FriendshipServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}
