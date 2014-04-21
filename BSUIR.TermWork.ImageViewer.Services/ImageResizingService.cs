// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageResizingService.cs" company="">
//   
// </copyright>
// <summary>
//   The image resizing service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.IO;

using BSUIR.TermWork.ImageViewer.Services.Contracts;

using ImageResizer;

namespace BSUIR.TermWork.ImageViewer.Services
{
    /// <summary>
    /// The image resizing service.
    /// </summary>
    public sealed class ImageResizingService : IImageResizingService
    {
        #region Public Methods and Operators

        /// <summary>
        /// The resize image.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
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

        #endregion
    }
}