// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageAlbumService.cs" company="">
//   
// </copyright>
// <summary>
//   The image album service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data;
using BSUIR.TermWork.ImageViewer.Data.Exceptions;
using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Validators;
using BSUIR.TermWork.ImageViewer.Services.Extensions;

namespace BSUIR.TermWork.ImageViewer.Services
{
    /// <summary>
    /// The image album service.
    /// </summary>
    public sealed class ImageAlbumService : ServiceBase, IImageAlbumService
    {
        #region Fields

        /// <summary>
        /// The _album validator.
        /// </summary>
        private readonly IEntityValidator<Album> _albumValidator;

        /// <summary>
        /// The _image resizing service.
        /// </summary>
        private readonly IImageResizingService _imageResizingService;

        /// <summary>
        /// The _image validator.
        /// </summary>
        private readonly IEntityValidator<Image> _imageValidator;

        /// <summary>
        /// The _user validator.
        /// </summary>
        private readonly IEntityValidator<User> _userValidator;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAlbumService"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        /// <param name="userValidator">
        /// The user validator.
        /// </param>
        /// <param name="albumValidator">
        /// The album validator.
        /// </param>
        /// <param name="imageValidator">
        /// The image validator.
        /// </param>
        /// <param name="imageResizingService">
        /// The image resizing service.
        /// </param>
        public ImageAlbumService(
            IUnitOfWork unitOfWork, 
            IEntityValidator<User> userValidator, 
            IEntityValidator<Album> albumValidator, 
            IEntityValidator<Image> imageValidator, 
            IImageResizingService imageResizingService) : base(unitOfWork)
        {
            this._userValidator = userValidator;
            this._albumValidator = albumValidator;
            this._imageValidator = imageValidator;
            this._imageResizingService = imageResizingService;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create album.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="album">
        /// The album.
        /// </param>
        /// <exception cref="ImageAlbumServiceException">
        /// </exception>
        public void CreateAlbum(User user, Album album)
        {
            this._albumValidator.Validate(album);
            this._userValidator.Validate(user);

            try
            {
                User owner = this.UnitOfWork.Repository<User, int>().FindByKey(user.Key);
                album.Owner = owner;
                album.ImageNumber = 0;
                album.CreationDate = DateTime.UtcNow;
                album.Tags = this.CreateTags(album.Name);
                var albumRepository = this.UnitOfWork.Repository<Album, int>() as IAlbumRepository;
                albumRepository.Insert(album);
                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new ImageAlbumServiceException(EntityValidationException.DefaultMessage, ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The get album by key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="Album"/>.
        /// </returns>
        /// <exception cref="ImageAlbumServiceException">
        /// </exception>
        public Album GetAlbumByKey(int key)
        {
            this._albumValidator.ValidateProperty(p => p.Key, key);
            Album result = null;

            try
            {
                result = this.UnitOfWork.Repository<Album, int>().FindByKey(key);
            }
            catch (DbException ex)
            {
                throw new ImageAlbumServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// The get album images.
        /// </summary>
        /// <param name="album">
        /// The album.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        /// <exception cref="ImageAlbumServiceException">
        /// </exception>
        public IList<Image> GetAlbumImages(Album album)
        {
            this._albumValidator.Validate(album);
            IList<Image> result = null;
            try
            {
                result =
                    this.UnitOfWork.Repository<Image, int>()
                        .Query()
                        .Include(p => p.Album)
                        .Include(p => p.ImageContent)
                        .Filter(p => p.Album.Key.Equals(album.Key))
                        .Get()
                        .ToList();
            }
            catch (DbException ex)
            {
                throw new ImageAlbumServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// The get albums by user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        /// <exception cref="ImageAlbumServiceException">
        /// </exception>
        public IList<Album> GetAlbumsByUser(User user)
        {
            IList<Album> result = null;
            this._userValidator.Validate(user);
            try
            {
                var albumRepository = this.UnitOfWork.Repository<Album, int>() as IAlbumRepository;
                result =
                    albumRepository.Query()
                                   .Include(p => p.Owner)
                                   .Filter(p => p.Owner.Key.Equals(user.Key))
                                   .Get()
                                   .ToList();
            }
            catch (DbException ex)
            {
                throw new ImageAlbumServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// The get albums by user key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        /// <exception cref="ImageAlbumServiceException">
        /// </exception>
        public IList<Album> GetAlbumsByUserKey(int key)
        {
            IList<Album> result = null;
            this._userValidator.ValidateProperty(p => p.Key, key);
            try
            {
                var albumRepository = this.UnitOfWork.Repository<Album, int>() as IAlbumRepository;
                result =
                    albumRepository.Query()
                                   .Include(p => p.Owner)
                                   .Filter(p => p.Owner.Key.Equals(key))
                                   .Get()
                                   .ToList();
            }
            catch (DbException ex)
            {
                throw new ImageAlbumServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// The get image by key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        /// <exception cref="ImageAlbumServiceException">
        /// </exception>
        public Image GetImageByKey(int key)
        {
            this._imageValidator.ValidateProperty(p => p.Key, key);
            Image result = null;

            try
            {
                result = this.UnitOfWork.Repository<Image, int>().FindByKey(key);
            }
            catch (DbException ex)
            {
                throw new ImageAlbumServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// The get random image from album.
        /// </summary>
        /// <param name="album">
        /// The album.
        /// </param>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        /// <exception cref="ImageAlbumServiceException">
        /// </exception>
        public Image GetRandomImageFromAlbum(Album album)
        {
            Image result = null;
            this._albumValidator.Validate(album);
            try
            {
                var imageRepository = this.UnitOfWork.Repository<Image, int>() as IImageRepository;
                result =
                    imageRepository.Query()
                                   .Include(p => p.ImageContent)
                                   .Include(p => p.Album)
                                   .Filter(p => p.Album.Key.Equals(album.Key))
                                   .Get()
                                   .FirstOrDefault();
            }
            catch (DbException ex)
            {
                throw new ImageAlbumServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// The remove album.
        /// </summary>
        /// <param name="album">
        /// The album.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ImageAlbumServiceException">
        /// </exception>
        public bool RemoveAlbum(Album album)
        {
            bool result = false;
            this._albumValidator.Validate(album);
            try
            {
                var albumRepository = this.UnitOfWork.Repository<Album, int>() as IAlbumRepository;
                Album albumToRemove = albumRepository.FindByKey(album.Key);
                if (albumToRemove != null)
                {
                    albumRepository.Delete(albumToRemove);
                    result = true;
                }

                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new ImageAlbumServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// The remove image.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ImageAlbumServiceException">
        /// </exception>
        public bool RemoveImage(Image image)
        {
            bool result = false;
            this._imageValidator.Validate(image);
            try
            {
                var imageRepository = this.UnitOfWork.Repository<Image, int>() as IImageRepository;
                Image imageToRemove = imageRepository.FindByKey(image.Key);
                if (imageToRemove != null)
                {
                    Album album = this.UnitOfWork.Repository<Album, int>()
                                      .FindByKey(image.Album.Key);
                    album.ImageNumber -= 1;
                    imageRepository.Delete(imageToRemove);
                    this.UnitOfWork.Repository<Album, int>().Update(album);
                    result = true;
                }

                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new ImageAlbumServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// The update album.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="album">
        /// The album.
        /// </param>
        /// <exception cref="ImageAlbumServiceException">
        /// </exception>
        public void UpdateAlbum(User user, Album album)
        {
            this._userValidator.Validate(user);
            this._albumValidator.Validate(album);

            try
            {
                var albumRepository = this.UnitOfWork.Repository<Album, int>() as IAlbumRepository;
                Album sourceAlbum =
                    albumRepository.Query()
                                   .Include(p => p.Tags)
                                   .Filter(p => p.Key.Equals(album.Key))
                                   .Get()
                                   .FirstOrDefault();
                if (sourceAlbum != null)
                {
                    sourceAlbum.Name = album.Name;
                    sourceAlbum.Description = album.Description;
                    this.RemoveTagsFromAlbum(album);
                    sourceAlbum.Tags = this.CreateTags(sourceAlbum.Name);
                    albumRepository.Update(sourceAlbum);
                    this.UnitOfWork.Save();
                }
            }
            catch (DbException ex)
            {
                throw new ImageAlbumServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        /// <summary>
        /// The update image.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="album">
        /// The album.
        /// </param>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <exception cref="ImageAlbumServiceException">
        /// </exception>
        public void UpdateImage(User user, Album album, Image image)
        {
            this._userValidator.Validate(user);
            this._albumValidator.Validate(album);
            this._imageValidator.Validate(image);

            try
            {
                User owner = this.UnitOfWork.Repository<User, int>().FindByKey(user.Key);
                Album sourceAlbum = this.UnitOfWork.Repository<Album, int>().FindByKey(album.Key);
                var imageRepository = this.UnitOfWork.Repository<Image, int>() as IImageRepository;
                Image imageSource =
                    imageRepository.Query()
                                   .Include(p => p.Owner)
                                   .Include(p => p.Album)
                                   .Include(p => p.Tags)
                                   .Filter(p => p.Key.Equals(image.Key))
                                   .Get()
                                   .FirstOrDefault();
                if (imageSource != null)
                {
                    imageSource.Name = image.Name;
                    imageSource.Description = image.Description;
                    imageSource.Owner = owner;
                    imageSource.Album = sourceAlbum;
                    this.RemoveTagsFromImage(image);
                    imageSource.Tags = this.CreateTags(imageSource.Name);
                    imageRepository.Update(imageSource);
                    this.UnitOfWork.Save();
                }
            }
            catch (DbException ex)
            {
                throw new ImageAlbumServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        /// <summary>
        /// The upload image.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <exception cref="ImageAlbumServiceException">
        /// </exception>
        public void UploadImage(Image image)
        {
            this._imageValidator.Validate(image);
            try
            {
                User owner = this.UnitOfWork.Repository<User, int>().FindByKey(image.Owner.Key);
                Album album = this.UnitOfWork.Repository<Album, int>().FindByKey(image.Album.Key);
                image.Tags = this.CreateTags(image.Name);
                image.ImageContent.Thumbnail =
                    this._imageResizingService.ResizeImage(
                        image.ImageContent.Content, 
                        ImageContent.MaxLengthFor.Width, 
                        ImageContent.MaxLengthFor.Height);
                image.Owner = owner;
                image.Album = album;
                var imageRepository = this.UnitOfWork.Repository<Image, int>() as IImageRepository;
                var albumRepository = this.UnitOfWork.Repository<Album, int>() as IAlbumRepository;
                album.ImageNumber += 1;
                imageRepository.Insert(image);
                albumRepository.Update(album);
                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new ImageAlbumServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The create new tags.
        /// </summary>
        /// <param name="tags">
        /// The tags.
        /// </param>
        /// <param name="tagRepository">
        /// The tag repository.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        private IList<Tag> CreateNewTags(IList<Tag> tags, IRepository<Tag, int> tagRepository)
        {
            if (tags == null)
            {
                throw new ArgumentNullException("tags");
            }

            IList<Tag> result = new List<Tag>(tags.Count);
            for (int i = 0; i < tags.Count; ++i)
            {
                string content = tags[i].Content;
                Tag temp =
                    tagRepository.Query()
                                 .Filter(p => p.Content.Equals(content, StringComparison.Ordinal))
                                 .Get()
                                 .FirstOrDefault();
                if (temp == null)
                {
                    tagRepository.Insert(tags[i]);
                    result.Add(tags[i]);
                }
                else
                {
                    result.Add(temp);
                }
            }

            return result;
        }

        /// <summary>
        /// The create tags.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        private IList<Tag> CreateTags(string name)
        {
            string[] words = name.SplitWords().RemoveDuplicates().ToArray();
            IList<Tag> tags =
                words.Where(p => p.ToLowerInvariant().Length >= Tag.MaxLengthFor.MinLength)
                     .Select(p => new Tag(p.ToLowerInvariant()))
                     .ToList();
            IRepository<Tag, int> tagRepository = this.UnitOfWork.Repository<Tag, int>();
            IList<Tag> resultTags = this.CreateNewTags(tags, tagRepository);
            return resultTags;
        }

        /// <summary>
        /// The remove tags from album.
        /// </summary>
        /// <param name="album">
        /// The album.
        /// </param>
        private void RemoveTagsFromAlbum(Album album)
        {
            IList<Tag> tags = album.Tags.ToList();
            for (int i = 0; i < tags.Count; ++i)
            {
                album.Tags.Remove(tags[i]);
            }
        }

        /// <summary>
        /// The remove tags from image.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        private void RemoveTagsFromImage(Image image)
        {
            IList<Tag> tags = image.Tags.ToList();
            for (int i = 0; i < tags.Count; ++i)
            {
                image.Tags.Remove(tags[i]);
            }
        }

        #endregion
    }
}