using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    internal sealed class ImageRepositoryBuilder : IRepositoryBuilder<Image, int>
    {
        #region Implementation of IRepositoryBuilder<Image,int>

        public IRepository<Image, int> Build(UnitOfWork unitOfWork)
        {
            return new ImageRepository(
                unitOfWork.Context,
                unitOfWork.Repository<ImageContent, int>(),
                unitOfWork.Repository<Comment, int>());
        }

        #endregion
    }
}
