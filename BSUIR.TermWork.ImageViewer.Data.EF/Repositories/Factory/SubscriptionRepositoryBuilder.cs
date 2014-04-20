using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    internal sealed class SubscriptionRepositoryBuilder : IRepositoryBuilder<Subscription, int>
    {
        #region Implementation of IRepositoryBuilder<Subscription,int>

        public IRepository<Subscription, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<Subscription, int>(unitOfWork.Context);
        }

        #endregion
    }
}
