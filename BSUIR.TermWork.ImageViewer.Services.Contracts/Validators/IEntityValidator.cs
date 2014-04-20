using System;
using System.Linq.Expressions;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Validators
{
    public interface IEntityValidator<TEntity>
        where TEntity : EntityBase

    {
        #region Public Methods and Operators

        void Validate(TEntity entity);

        void ValidateProperty<T>(Expression<Func<TEntity, T>> property, T propertyValue);

        #endregion
    }
}