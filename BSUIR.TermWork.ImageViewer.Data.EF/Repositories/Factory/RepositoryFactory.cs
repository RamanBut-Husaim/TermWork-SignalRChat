// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryFactory.cs" company="">
//   
// </copyright>
// <summary>
//   The repository factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections;

using BSUIR.TermWork.ImageViewer.Data.Exceptions;
using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Data.Repositories.Factory;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory
{
    /// <summary>
    /// The repository factory.
    /// </summary>
    public sealed class RepositoryFactory : IRepositoryFactory
    {
        #region Static Fields

        /// <summary>
        /// The lazy.
        /// </summary>
        private static readonly Lazy<RepositoryFactory> lazy =
            new Lazy<RepositoryFactory>(() => new RepositoryFactory(), true);

        /// <summary>
        /// The sync.
        /// </summary>
        private static readonly object sync = new object();

        #endregion

        #region Fields

        /// <summary>
        /// The _repository builders.
        /// </summary>
        private readonly Hashtable _repositoryBuilders;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="RepositoryFactory"/> class from being created.
        /// </summary>
        private RepositoryFactory()
        {
            this._repositoryBuilders = new Hashtable();

            this._repositoryBuilders.Add(typeof(Role).Name, new RoleRepositoryBuilder());
            this._repositoryBuilders.Add(typeof(User).Name, new UserRepositoryBuilder());
            this._repositoryBuilders.Add(typeof(Profile).Name, new ProfileRepositoryBuilder());
            this._repositoryBuilders.Add(typeof(Comment).Name, new CommentRepositoryBuilder());
            this._repositoryBuilders.Add(typeof(Tag).Name, new TagRepositoryBuilder());
            this._repositoryBuilders.Add(typeof(Image).Name, new ImageRepositoryBuilder());
            this._repositoryBuilders.Add(
                typeof(ImageContent).Name, 
                new ImageContentRepositoryBuilder());
            this._repositoryBuilders.Add(
                typeof(Subscription).Name, 
                new SubscriptionRepositoryBuilder());
            this._repositoryBuilders.Add(
                typeof(AccessRight).Name, 
                new AccessRightRepositoryBuilder());
            this._repositoryBuilders.Add(typeof(Album).Name, new AlbumRepositoryBuilder());
            this._repositoryBuilders.Add(
                typeof(SubscriptionType).Name, 
                new SubscriptionTypeRepositoryBuilder());
            this._repositoryBuilders.Add(typeof(Message).Name, new MessageRepositoryBuilder());
            this._repositoryBuilders.Add(typeof(Friendship).Name, new FriendshipRepositoryBuilder());
            this._repositoryBuilders.Add(
                typeof(FriendshipRequest).Name, 
                new FriendshipRequestRepositoryBuilder());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static IRepositoryFactory Instance
        {
            get { return lazy.Value; }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The build repository.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <typeparam name="TKey">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IRepository"/>.
        /// </returns>
        /// <exception cref="RepositoryFactoryException">
        /// </exception>
        public IRepository<TEntity, TKey> BuildRepository<TEntity, TKey>(IUnitOfWork unitOfWork)
            where TEntity : Entity<TKey>
        {
            IRepository<TEntity, TKey> result = null;

            string entityType = typeof(TEntity).Name;
            lock (sync)
            {
                var repositoryBuilder =
                    this._repositoryBuilders[entityType] as IRepositoryBuilder<TEntity, TKey>;
                if (repositoryBuilder != null)
                {
                    result = repositoryBuilder.Build(unitOfWork as UnitOfWork);
                }
                else
                {
                    throw new RepositoryFactoryException(
                        string.Format(
                            "The repository builder for entity-key pair {0}-{1} cannot be found!", 
                            typeof(TEntity).FullName, 
                            typeof(TKey).FullName));
                }
            }

            return result;
        }

        #endregion
    }
}