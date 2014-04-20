using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    internal sealed class FriendshipRepositoryBuilder : IRepositoryBuilder<Friendship, int>
    {
        #region Implementation of IRepositoryBuilder<Friendship,int>

        public IRepository<Friendship, int> Build(UnitOfWork unitOfWork)
        {
            return new FriendshipRepository(
                unitOfWork.Context,
                unitOfWork.Repository<Message, int>());
        }

        #endregion
    }
}
