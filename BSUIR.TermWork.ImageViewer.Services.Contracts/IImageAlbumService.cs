using System.Collections.Generic;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts
{
    public interface IImageAlbumService : IService
    {
        #region Public Methods and Operators

        void CreateAlbum(User user, Album album);

        Album GetAlbumByKey(int key);

        IList<Image> GetAlbumImages(Album album);

        IList<Album> GetAlbumsByUser(User user);

        IList<Album> GetAlbumsByUserKey(int key);

        Image GetImageByKey(int key);

        Image GetRandomImageFromAlbum(Album album);

        bool RemoveAlbum(Album album);

        bool RemoveImage(Image image);

        void UpdateAlbum(User user, Album album);

        void UpdateImage(User user, Album album, Image image);

        void UploadImage(Image image);

        #endregion
    }
}