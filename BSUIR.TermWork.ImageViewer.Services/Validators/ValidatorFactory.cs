// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorFactory.cs" company="">
//   
// </copyright>
// <summary>
//   The validator factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Validators;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    /// <summary>
    /// The validator factory.
    /// </summary>
    public sealed class ValidatorFactory : IValidatorFactory
    {
        #region Static Fields

        /// <summary>
        /// The lazy.
        /// </summary>
        private static readonly Lazy<ValidatorFactory> lazy =
            new Lazy<ValidatorFactory>(() => new ValidatorFactory(), true);

        /// <summary>
        /// The sync.
        /// </summary>
        private static readonly object sync = new object();

        #endregion

        #region Fields

        /// <summary>
        /// The _validators pool.
        /// </summary>
        private readonly Dictionary<string, object> _validatorsPool;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="ValidatorFactory"/> class from being created.
        /// </summary>
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
            this._validatorsPool.Add(typeof(Message).Name, new MessageValidator());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static IValidatorFactory Instance
        {
            get { return lazy.Value; }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The build entity validator.
        /// </summary>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEntityValidator"/>.
        /// </returns>
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