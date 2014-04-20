using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    internal sealed class AlbumRepositoryBuilder : IRepositoryBuilder<Album, int>
    {
        #region Implementation of IRepositoryBuilder<Album,int>

        public IRepository<Album, int> Build(UnitOfWork unitOfWork)
        {
            return new AlbumRepository(
                unitOfWork.Context,
                unitOfWork.Repository<Image, int>() as IImageRepository);
        }

        #endregion
    }
}
