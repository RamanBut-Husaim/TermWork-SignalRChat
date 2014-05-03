using System;
using System.IO;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Album;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    public sealed class AlbumMapper : IAlbumMapper
    {
        #region Fields

        private readonly IImageAlbumService _imageAlbumService;
        private readonly IImageConverter _imageConverter;
        private readonly string _pathToContentFolder;

        #endregion

        #region Constructors and Destructors

        public AlbumMapper(IImageAlbumService imageAlbumService, IImageConverter imageConverter)
        {
            this._imageAlbumService = imageAlbumService;
            this._imageConverter = imageConverter;
            string temp = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
            this._pathToContentFolder = Path.Combine(temp, @"BSUIR.TermWork.ImageViewer.UI.WebSite\Content");
        }

        #endregion

        #region Public Methods and Operators

        public AlbumHeaderViewModel BuildAlbumHeader(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException("album");
            }

            var result = new AlbumHeaderViewModel();
            result.Name = album.Name;
            result.ImageNumber = album.ImageNumber;
            result.Key = album.Key;

            return result;
        }

        public AlbumListItemViewModel BuildListItem(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException("album");
            }

            var result = new AlbumListItemViewModel();
            result.Name = album.Name;
            result.Description = album.Description;
            result.Key = album.Key;
            result.ImageNumber = album.ImageNumber;

            try
            {
                Image randomImage = this._imageAlbumService.GetRandomImageFromAlbum(album);
                if (randomImage == null)
                {
                    result.ThumbnailContentType = "image/png";
                }
                else
                {
                    result.ThumbnailContentType = randomImage.ContentType;
                }

                result.Thumbnail = string.Format(
                    "data:{0};base64,{1}",
                    result.ThumbnailContentType,
                    this.GetImageContent(randomImage));
            }
            catch (Exception)
            {
                result.Thumbnail = string.Empty;
                result.ThumbnailContentType = string.Empty;
            }

            return result;
        }

        public AlbumCreateViewModel BuildCreateAlbum(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException("album");
            }

            var result = new AlbumCreateViewModel();
            result.Name = album.Name;
            result.Description = album.Description;
            result.Key = album.Key;
            return result;
        }

        public void UpdateAlbum(Album album, AlbumCreateViewModel viewModel)
        {
            if (album == null)
            {
                throw new ArgumentNullException("album");
            }

            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            album.Name = viewModel.Name;
            album.Description = viewModel.Description;
        }

        public AlbumDetailedViewModel BuildDetailed(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException("album");
            }

            var result = new AlbumDetailedViewModel();
            result.CreationDate = album.CreationDate;
            result.ImageNumber = album.ImageNumber;
            result.Name = album.Name;
            result.Description = album.Description;
            result.Key = album.Key;

            return result;
        }

        public Album BuildAlbum(AlbumCreateViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            var result = new Album();
            result.ImageNumber = 0;
            result.Name = viewModel.Name;
            result.Description = viewModel.Description;
            result.CreationDate = DateTime.UtcNow;

            return result;
        }

        #endregion

        #region Methods

        private string GetImageContent(Image randomImage)
        {
            string result;
            if (randomImage != null)
            {
                using (var stream = new MemoryStream())
                {
                    stream.Write(randomImage.ImageContent.Thumbnail, 0, randomImage.ImageContent.Thumbnail.Length);
                    result = this._imageConverter.ConvertToBase64(stream);
                }
            }
            else
            {
                string imagePath = Path.Combine(this._pathToContentFolder, @"images\profile_pages\no_preview.png");
                using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    result = this._imageConverter.ConvertToBase64(stream);
                }
            }

            return result;
        }

        #endregion
    }
}