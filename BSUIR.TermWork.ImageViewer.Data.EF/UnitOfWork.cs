// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="">
//   
// </copyright>
// <summary>
//   The unit of work.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections;
using System.Threading.Tasks;

using BSUIR.TermWork.ImageViewer.Data.Exceptions;
using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Data.Repositories.Factory;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF
{
    /// <summary>
    /// The unit of work.
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        #region Fields

        /// <summary>
        /// The _context.
        /// </summary>
        private readonly IDbContext _context;

        /// <summary>
        /// The _repositories.
        /// </summary>
        private readonly Hashtable _repositories;

        /// <summary>
        /// The _repository factory.
        /// </summary>
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// The _is disposed.
        /// </summary>
        private bool _isDisposed;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="repositoryFactory">
        /// The repository factory.
        /// </param>
        public UnitOfWork(IDbContext context, IRepositoryFactory repositoryFactory)
        {
            this._context = context;
            this._repositories = new Hashtable();
            this._repositoryFactory = repositoryFactory;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the context.
        /// </summary>
        internal IDbContext Context
        {
            get { return this._context; }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        public void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }

            this._isDisposed = true;
        }

        /// <summary>
        /// The repository.
        /// </summary>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <typeparam name="TKey">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IRepository"/>.
        /// </returns>
        public IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : Entity<TKey>
        {
            string type = typeof(TEntity).Name;
            if (!this._repositories.ContainsKey(type))
            {
                object repositoryInstance =
                    this._repositoryFactory.BuildRepository<TEntity, TKey>(this);
                this._repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity, TKey>)this._repositories[type];
        }

        /// <summary>
        /// The save.
        /// </summary>
        /// <exception cref="DbException">
        /// </exception>
        public void Save()
        {
            try
            {
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DbException(ex);
            }
        }

        /// <summary>
        /// The save async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="DbException">
        /// </exception>
        public async Task SaveAsync()
        {
            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DbException(ex);
            }
        }

        #endregion
    }
}