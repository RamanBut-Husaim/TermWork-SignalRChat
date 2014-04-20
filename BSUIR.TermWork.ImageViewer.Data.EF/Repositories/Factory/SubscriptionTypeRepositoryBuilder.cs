using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    internal sealed class SubscriptionTypeRepositoryBuilder :
        IRepositoryBuilder<SubscriptionType, int>
    {
        #region Implementation of IRepositoryBuilder<SubscriptionType,int>

        public IRepository<SubscriptionType, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<SubscriptionType, int>(unitOfWork.Context);
        }

        #endregion
    }
}
