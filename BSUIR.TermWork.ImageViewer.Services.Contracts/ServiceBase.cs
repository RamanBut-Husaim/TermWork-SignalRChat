using BSUIR.TermWork.ImageViewer.Data;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts
{
    public abstract class ServiceBase : IService
    {
        private readonly IUnitOfWork _unitOfWork;

        protected IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }

        protected ServiceBase(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
    }
}
