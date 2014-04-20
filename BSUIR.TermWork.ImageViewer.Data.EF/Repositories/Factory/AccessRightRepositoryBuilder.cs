using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    internal sealed class AccessRightRepositoryBuilder : IRepositoryBuilder<AccessRight, int>
    {
        #region Implementation of IRepositoryBuilder<AccessRight,int>

        public IRepository<AccessRight, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<AccessRight, int>(unitOfWork.Context);
        }

        #endregion
    }
}
