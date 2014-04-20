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
    public sealed class ImageAlbumService : ServiceBase, IImageAlbumService
    {
        private readonly IEntityValidator<User> _userValidator;
        private readonly IEntityValidator<Album> _albumValidator;
        private readonly IEntityValidator<Image> _imageValidator;
        private readonly IImageResizingService _imageResizingService;

        public ImageAlbumService(
            IUnitOfWork unitOfWork,
            IEntityValidator<User> userValidator,
            IEntityValidator<Album> albumValidator,
            IEntityValidator<Image> imageValidator,
            IImageResizingService imageResizingService)
            : base(unitOfWork)
        {
            this._userValidator = userValidator;
            this._albumValidator = albumValidator;
            this._imageValidator = imageValidator;
            this._imageResizingService = imageResizingService;
        }

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

        public void UpdateImage(User user, Album album, Image image)
        {
            this._userValidator.Validate(user);
            this._albumValidator.Validate(album);
            this._imageValidator.Validate(image);

            try
            {
                User owner = this.UnitOfWork.Repository<User, int>().FindByKey(user.Key);
                Album sourceAlbum = this.UnitOfWork.Repository<Album, int>().FindByKey(album.Key);
                IImageRepository imageRepository = this.UnitOfWork.Repository<Image, int>() as IImageRepository;
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

        public bool RemoveAlbum(Album album)
        {
            bool result = false;
            this._albumValidator.Validate(album);
            try
            {
                IAlbumRepository albumRepository = this.UnitOfWork.Repository<Album, int>() as IAlbumRepository;
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

        public bool RemoveImage(Image image)
        {
            bool result = false;
            this._imageValidator.Validate(image);
            try
            {
                IImageRepository imageRepository = this.UnitOfWork.Repository<Image, int>() as IImageRepository;
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
                IImageRepository imageRepository = this.UnitOfWork.Repository<Image, int>() as IImageRepository;
                IAlbumRepository albumRepository = this.UnitOfWork.Repository<Album, int>() as IAlbumRepository;
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

        private void RemoveTagsFromAlbum(Album album)
        {
            IList<Tag> tags = album.Tags.ToList();
            for (int i = 0; i < tags.Count; ++i)
            {
                album.Tags.Remove(tags[i]);
            }
        }

        private void RemoveTagsFromImage(Image image)
        {
            IList<Tag> tags = image.Tags.ToList();
            for (int i = 0; i < tags.Count; ++i)
            {
                image.Tags.Remove(tags[i]);
            }
        }

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
    }
}
