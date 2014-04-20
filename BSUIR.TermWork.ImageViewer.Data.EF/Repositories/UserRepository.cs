using System;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories
{
    public sealed class UserRepository : Repository<User, int>, IUserRepository
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IAlbumRepository _albumRepository;

        public UserRepository(
            IDbContext context,
            IProfileRepository profileRepository,
            IAlbumRepository albumRepository)
            : base(context)
        {
            this._profileRepository = profileRepository;
            this._albumRepository = albumRepository;
        }

        #region Overrides of Repository<User,int>

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
