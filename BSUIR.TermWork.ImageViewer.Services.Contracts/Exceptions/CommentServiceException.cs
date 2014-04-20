using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    using System.Runtime.Serialization;

    [Serializable]
    public class CommentServiceException : EntityValidationException
    {
        public CommentServiceException()
		{
		}

		public CommentServiceException(string message = DefaultMessage)
			: base(message)
		{
		}

		public CommentServiceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

        protected CommentServiceException(SerializationInfo info,
			StreamingContext context)
			: base(info, context)
		{
		}
    }
}
