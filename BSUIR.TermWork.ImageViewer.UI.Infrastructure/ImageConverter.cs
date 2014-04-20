using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure
{
    using System.IO;

    public sealed class ImageConverter : IImageConverter
    {
        public string ConvertToBase64(Stream imageStream)
        {
            if (imageStream == null)
            {
                throw new ArgumentNullException("imageStream");
            }
            imageStream.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[imageStream.Length];
            imageStream.Read(buffer, 0, buffer.Length);
            string result = Convert.ToBase64String(buffer);
            return result;
        }
    }
}
