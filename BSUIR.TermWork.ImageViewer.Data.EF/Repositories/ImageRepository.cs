// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The image repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories
{
    /// <summary>
    /// The image repository.
    /// </summary>
    public sealed class ImageRepository : Repository<Image, int>, IImageRepository
    {
        #region Fields

        /// <summary>
        /// The _comment repository.
        /// </summary>
        private readonly IRepository<Comment, int> _commentRepository;

        /// <summary>
        /// The _image content repository.
        /// </summary>
        private readonly IRepository<ImageContent, int> _imageContentRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="imageContentRepository">
        /// The image content repository.
        /// </param>
        /// <param name="commentRepository">
        /// The comment repository.
        /// </param>
        public ImageRepository(
            IDbContext context, 
            IRepository<ImageContent, int> imageContentRepository, 
            IRepository<Comment, int> commentRepository) : base(context)
        {
            this._imageContentRepository = imageContentRepository;
            this._commentRepository = commentRepository;
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
        public override void Delete(Image entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            ImageContent[] imageContents =
                this._imageContentRepository.Query()
                    .Filter(p => entity.ImageContent.Key.Equals(p.Key))
                    .Get()
                    .ToArray();
            Array.ForEach(imageContents, this._imageContentRepository.Delete);
            Comment[] comments =
                this._commentRepository.Query()
                    .Filter(p => entity.Key.Equals(p.Image.Key))
                    .Get()
                    .ToArray();
            Array.ForEach(comments, this._commentRepository.Delete);

            base.Delete(entity);
        }

        #endregion
    }
}