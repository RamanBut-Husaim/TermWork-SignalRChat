using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.TermWork.ImageViewer.Shared.Exceptions
{
    using System.Runtime.Serialization;

    [Serializable]
    public class ImageViewerException : Exception
    {
        public ImageViewerException()
		{
		}

		public ImageViewerException(string message)
			: base(message)
		{
		}

		public ImageViewerException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

        protected ImageViewerException(SerializationInfo info,
			StreamingContext context)
			: base(info, context)
		{
		}
    }
}
