using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Validators
{
    public interface IValidatorFactory
    {
        #region Public Methods and Operators

        IEntityValidator<TEntity> BuildEntityValidator<TEntity>() where TEntity : EntityBase;

        #endregion
    }
}