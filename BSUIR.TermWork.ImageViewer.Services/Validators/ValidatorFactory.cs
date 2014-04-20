using System;
using System.Collections.Generic;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Validators;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    public sealed class ValidatorFactory : IValidatorFactory
    {
        private static readonly Lazy<ValidatorFactory> lazy =
            new Lazy<ValidatorFactory>(() => new ValidatorFactory(), true);

        private readonly Dictionary<string, object> _validatorsPool;
        private static readonly object sync = new object();

        public static IValidatorFactory Instance
        {
            get { return lazy.Value; }
        }

        private ValidatorFactory()
        {
            this._validatorsPool = new Dictionary<string, object>();
            this._validatorsPool.Add(typeof(User).Name, new UserValidator());
            this._validatorsPool.Add(typeof(Role).Name, new RoleValidator());
            this._validatorsPool.Add(typeof(Profile).Name, new ProfileValidator());
            this._validatorsPool.Add(typeof(AccessRight).Name, new AccessRightValidator());
            this._validatorsPool.Add(typeof(Album).Name, new AlbumValidator());
            this._validatorsPool.Add(typeof(Image).Name, new ImageValidator());
            this._validatorsPool.Add(typeof(Comment).Name, new CommentValidator());
        }

        #region Implementation of IValidatorFactory

        public IEntityValidator<TEntity> BuildEntityValidator<TEntity>() where TEntity : EntityBase
        {
            IEntityValidator<TEntity> result = null;
            string entityTypeName = typeof(TEntity).Name;
            lock (sync)
            {
                object tempResult;
                if (this._validatorsPool.TryGetValue(entityTypeName, out tempResult))
                {
                    result = tempResult as IEntityValidator<TEntity>;
                }
            }
            return result;
        }

        #endregion
    }
}
