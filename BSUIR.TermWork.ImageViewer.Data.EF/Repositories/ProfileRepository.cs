// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProfileRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The profile repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories
{
    /// <summary>
    /// The profile repository.
    /// </summary>
    public sealed class ProfileRepository : Repository<Profile, int>, IProfileRepository
    {
        #region Fields

        /// <summary>
        /// The _friendship repository.
        /// </summary>
        private readonly IFriendshipRepository _friendshipRepository;

        /// <summary>
        /// The _subscription repository.
        /// </summary>
        private readonly IRepository<Subscription, int> _subscriptionRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="subscriptionRepository">
        /// The subscription repository.
        /// </param>
        /// <param name="friendshipRepository">
        /// The friendship repository.
        /// </param>
        public ProfileRepository(
            IDbContext context, 
            IRepository<Subscription, int> subscriptionRepository, 
            IFriendshipRepository friendshipRepository) : base(context)
        {
            this._subscriptionRepository = subscriptionRepository;
            this._friendshipRepository = friendshipRepository;
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

        #endregion

        #region Methods

        /// <summary>
        /// The remove friendship links.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
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

            foreach (Friendship link in friendshipLinks)
            {
                this._friendshipRepository.Delete(link);
            }
        }

        #endregion
    }
}