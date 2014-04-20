using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    internal sealed class ImageContentRepositoryBuilder : IRepositoryBuilder<ImageContent, int>
    {
        #region Implementation of IRepositoryBuilder<ImageContent,int>

        public IRepository<ImageContent, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<ImageContent, int>(unitOfWork.Context);
        }

        #endregion
    }
}
