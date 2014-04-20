using System;
using System.IO;

using BSUIR.TermWork.ImageViewer.Services.Contracts;

using ImageResizer;

namespace BSUIR.TermWork.ImageViewer.Services
{
    public sealed class ImageResizingService : IImageResizingService
    {
        public byte[] ResizeImage(byte[] source, int width, int height)
        {
            byte[] result = null;
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            using (var sourceStream = new MemoryStream(source.Length))
            {
                sourceStream.Write(source, 0, source.Length);
                sourceStream.Seek(0, SeekOrigin.Begin);
                var settings = new ResizeSettings();
                settings.Width = width;
                settings.Height = height;
                settings.Mode = FitMode.Crop;
                using (var destinationStream = new MemoryStream())
                {
                    ImageBuilder.Current.Build(sourceStream, destinationStream, settings);
                    result = destinationStream.ToArray();
                }
                return result;
            }
        }
    }
}
