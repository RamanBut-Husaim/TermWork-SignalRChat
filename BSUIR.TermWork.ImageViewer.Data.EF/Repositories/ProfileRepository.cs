using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories
{
    public sealed class ProfileRepository : Repository<Profile, int>, IProfileRepository
    {
        private readonly IRepository<Subscription, int> _subscriptionRepository;
        private readonly IFriendshipRepository _friendshipRepository;

        public ProfileRepository(
            IDbContext context,
            IRepository<Subscription, int> subscriptionRepository,
            IFriendshipRepository friendshipRepository) : base(context)
        {
            this._subscriptionRepository = subscriptionRepository;
            this._friendshipRepository = friendshipRepository;
        }

        public override void Delete(Profile entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Subscription[] subscriptions = entity.Subscriptions.ToArray();
            Array.ForEach(subscriptions, this._subscriptionRepository.Delete);
            this.RemoveFriendshipLinks(entity);

            base.Delete(entity);
        }

        private void RemoveFriendshipLinks(Profile entity)
        {
            IEnumerable<Friendship> friendshipLinks =
                this._friendshipRepository.Query()
                    .Filter(
                        p =>
                        p.FirstProfile.Key.Equals(entity.Key)
                        || p.SecondProfile.Key.Equals(entity.Key))
                    .Include(p => p.FirstProfile)
                    .Include(p => p.SecondProfile)
                    .Get();

            foreach (var link in friendshipLinks)
            {
                this._friendshipRepository.Delete(link);
            }
        }
    }
}
