using System;
using System.IO;
using System.Web;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Image;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    public class ImageMapper : IImageMapper
    {
        private readonly IImageConverter _imageConverter;

        public ImageMapper(IImageConverter imageConverter)
        {
            this._imageConverter = imageConverter;
        }

        public ImageSliderViewModel BuildSlider(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            var result = new ImageSliderViewModel();
            result.Name = image.Name;
            result.Description = image.Description;
            result.Key = image.Key;
            result.Rate = image.Rate;

            try
            {
                result.Content = string.Format(
                    "data:{0};base64,{1}",
                    image.ContentType,
                    this.GetImageThumbnailContent(image));
            }
            catch (Exception ex)
            {
                result.Content = string.Empty;
            }

            return result;
        }

        public ImagePreviewViewModel BuildPreview(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            var result = new ImagePreviewViewModel();
            result.Name = image.Name;
            result.Description = image.Description;
            result.Key = image.Key;
            try
            {
                result.Content = string.Format(
                    "data:{0};base64,{1}",
                    image.ContentType,
                    this.GetImageContent(image));
            }
            catch (Exception ex)
            {
                result.Content = string.Empty;
            }

            return result;
        }

        private string GetImageThumbnailContent(Image image)
        {
            string result = null;
            using (var stream = new MemoryStream())
            {
                stream.Write(image.ImageContent.Thumbnail, 0, image.ImageContent.Thumbnail.Length);
                result = this._imageConverter.ConvertToBase64(stream);
            }

            return result;
        }

        private string GetImageContent(Image image)
        {
            string result = null;
            using (var stream = new MemoryStream())
            {
                stream.Write(image.ImageContent.Content, 0, image.ImageContent.Content.Length);
                result = this._imageConverter.ConvertToBase64(stream);
            }

            return result;
        }

        public ImageEditViewModel BuildEdit(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }
            var result = new ImageEditViewModel();
            result.Name = image.Name;
            result.Description = image.Description;
            result.Key = image.Key;
            return result;
        }

        public void UpdateImage(Image image, ImageEditViewModel viewModel)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            image.Name = viewModel.Name;
            image.Description = viewModel.Description;
        }

        public ImageDetailedViewModel BuildDetailed(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            var result = new ImageDetailedViewModel();
            result.Key = image.Key;
            result.Name = image.Name;
            result.Rate = image.Rate;
            result.AlbumName = image.Album.Name;
            result.UploadDate = image.UploadDate;
            result.UserName = string.Format(
                "{0} {1}",
                image.Owner.UserProfile.FirstName,
                image.Owner.UserProfile.LastName);
            return result;
        }

        public Image BuildImage(HttpPostedFileBase uploadedImage)
        {
            if (uploadedImage == null)
            {
                throw new ArgumentNullException("uploadedImage");
            }

            var result = new Image();
            result.Extension = this.GetExtension(uploadedImage.FileName);
            result.Name = uploadedImage.FileName.Replace(result.Extension, string.Empty);
            result.ContentType = uploadedImage.ContentType;
            result.Rate = 0;
            result.UploadDate = DateTime.UtcNow;
            result.Description = uploadedImage.FileName;
            result.ImageContent = this.GenerateImageContent(uploadedImage);
            return result;
        }

        private string GetExtension(string name)
        {
            return Path.GetExtension(name);
        }

        private ImageContent GenerateImageContent(HttpPostedFileBase fileBase)
        {
            var result = new ImageContent();
            using (var memoryStream = new MemoryStream(fileBase.ContentLength))
            {
                fileBase.InputStream.CopyTo(memoryStream);
                result.Content = memoryStream.ToArray();
            }

            return result;
        }
    }
}