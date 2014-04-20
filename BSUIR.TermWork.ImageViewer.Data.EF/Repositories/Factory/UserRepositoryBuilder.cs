using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    internal sealed class UserRepositoryBuilder : IRepositoryBuilder<User, int>
    {
        #region Implementation of IRepositoryBuilder<User,int>

        public IRepository<User, int> Build(UnitOfWork unitOfWork)
        {
            return new UserRepository(
                unitOfWork.Context,
                unitOfWork.Repository<Profile, int>() as IProfileRepository,
                unitOfWork.Repository<Album, int>() as IAlbumRepository);
        }

        #endregion
    }
}
