// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlbumRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The album repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories
{
    /// <summary>
    /// The album repository.
    /// </summary>
    public sealed class AlbumRepository : Repository<Album, int>, IAlbumRepository
    {
        #region Fields

        /// <summary>
        /// The _image repository.
        /// </summary>
        private readonly IImageRepository _imageRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="imageRepository">
        /// The image repository.
        /// </param>
        public AlbumRepository(IDbContext context, IImageRepository imageRepository) : base(context)
        {
            this._imageRepository = imageRepository;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
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