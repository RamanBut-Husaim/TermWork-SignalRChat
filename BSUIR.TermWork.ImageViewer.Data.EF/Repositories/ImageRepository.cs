using System;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories
{
    public sealed class ImageRepository : Repository<Image, int>, IImageRepository
    {
        private readonly IRepository<ImageContent, int> _imageContentRepository;
        private readonly IRepository<Comment, int> _commentRepository;

        public ImageRepository(
            IDbContext context,
            IRepository<ImageContent, int> imageContentRepository,
            IRepository<Comment, int> commentRepository)
            : base(context)
        {
            this._imageContentRepository = imageContentRepository;
            this._commentRepository = commentRepository;
        }

        #region Overrides of Repository<Image,int>

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
