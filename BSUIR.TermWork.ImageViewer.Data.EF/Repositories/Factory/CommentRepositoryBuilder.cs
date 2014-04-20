using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    internal sealed class CommentRepositoryBuilder : IRepositoryBuilder<Comment, int>
    {
        #region Implementation of IRepositoryBuilder<Comment,int>

        public IRepository<Comment, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<Comment, int>(unitOfWork.Context);
        }

        #endregion
    }
}
