using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    internal sealed class FriendshipRequestRepositoryBuilder : IRepositoryBuilder<FriendshipRequest, int>
    {
        #region Implementation of IRepositoryBuilder<FriendshipRequest,int>

        public IRepository<FriendshipRequest, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<FriendshipRequest, int>(unitOfWork.Context);
        }

        #endregion
    }
}
