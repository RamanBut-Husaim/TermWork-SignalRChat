using System;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories
{
    public sealed class AlbumRepository : Repository<Album, int>, IAlbumRepository
    {
        private readonly IImageRepository _imageRepository;

        #region Constructors and Destructors

        public AlbumRepository(IDbContext context, IImageRepository imageRepository)
            : base(context)
        {
            this._imageRepository = imageRepository;
        }

        public override void Delete(Album entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Image[] images =
                this._imageRepository.Query()
                    .Filter(p => entity.Key.Equals(p.Album.Key))
                    .Get()
                    .ToArray();
            Array.ForEach(images, this._imageRepository.Delete);
            base.Delete(entity);
        }

        #endregion
    }
}