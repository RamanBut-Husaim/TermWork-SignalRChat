using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    internal sealed class ProfileRepositoryBuilder : IRepositoryBuilder<Profile, int>
    {
        #region Implementation of IRepositoryBuilder<Profile,int>

        public IRepository<Profile, int> Build(UnitOfWork unitOfWork)
        {
            return new ProfileRepository(
                unitOfWork.Context,
                unitOfWork.Repository<Subscription, int>(),
                unitOfWork.Repository<Friendship, int>() as IFriendshipRepository);
        }

        #endregion
    }
}
