// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The user repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories
{
    /// <summary>
    /// The user repository.
    /// </summary>
    public sealed class UserRepository : Repository<User, int>, IUserRepository
    {
        #region Fields

        /// <summary>
        /// The _album repository.
        /// </summary>
        private readonly IAlbumRepository _albumRepository;

        /// <summary>
        /// The _profile repository.
        /// </summary>
        private readonly IProfileRepository _profileRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="profileRepository">
        /// The profile repository.
        /// </param>
        /// <param name="albumRepository">
        /// The album repository.
        /// </param>
        public UserRepository(
            IDbContext context, 
            IProfileRepository profileRepository, 
            IAlbumRepository albumRepository) : base(context)
        {
            this._profileRepository = profileRepository;
            this._albumRepository = albumRepository;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public override void Delete(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            Profile[] profiles =
                this._profileRepository.Query()
                    .Filter(p => entity.Key.Equals(p.User.Key))
                    .Get()
                    .ToArray();
            Album[] albums =
                this._albumRepository.Query()
                    .Filter(p => entity.Key.Equals(p.Owner.Key))
                    .Get()
                    .ToArray();
            Array.ForEach(profiles, this._profileRepository.Delete);
            Array.ForEach(albums, this._albumRepository.Delete);

            base.Delete(entity);
        }

        #endregion
    }
}