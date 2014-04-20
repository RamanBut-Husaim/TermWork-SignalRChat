using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    internal sealed class TagRepositoryBuilder : IRepositoryBuilder<Tag, int>
    {
        #region Implementation of IRepositoryBuilder<Tag,int>

        public IRepository<Tag, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<Tag, int>(unitOfWork.Context);
        }

        #endregion
    }
}
