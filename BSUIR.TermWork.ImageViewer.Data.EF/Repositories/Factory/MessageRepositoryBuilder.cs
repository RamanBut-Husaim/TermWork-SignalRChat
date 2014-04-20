using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    internal sealed class MessageRepositoryBuilder : IRepositoryBuilder<Message, int>
    {
        #region Implementation of IRepositoryBuilder<Message,int>

        public IRepository<Message, int> Build(UnitOfWork unitOfWork)
        {
            return new Repository<Message, int>(unitOfWork.Context);
        }

        #endregion
    }
}
