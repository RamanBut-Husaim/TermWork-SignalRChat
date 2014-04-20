using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    public sealed class RoleRepositoryBuilder : IRepositoryBuilder<Role, int>
    {
        #region Implementation of IRepositoryBuilder<Role,int>

        public IRepository<Role, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<Role, int>(unitOfWork.Context);
        }

        #endregion
    }
}
