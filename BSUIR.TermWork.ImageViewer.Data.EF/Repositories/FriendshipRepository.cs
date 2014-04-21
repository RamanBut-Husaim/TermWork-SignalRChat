// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FriendshipRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The friendship repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories
{
    /// <summary>
    /// The friendship repository.
    /// </summary>
    public sealed class FriendshipRepository : Repository<Friendship, int>, IFriendshipRepository
    {
        #region Fields

        /// <summary>
        /// The _message repository.
        /// </summary>
        private readonly IRepository<Message, int> _messageRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="messageRepository">
        /// The message repository.
        /// </param>
        public FriendshipRepository(IDbContext context, IRepository<Message, int> messageRepository)
            : base(context)
        {
            this._messageRepository = messageRepository;
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
        public override void Delete(Friendship entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Friendship entityToDelete =
                this.Query()
                    .Filter(p => p.Key == entity.Key)
                    .Include(p => p.Messages)
                    .Get()
                    .FirstOrDefault();
            if (entityToDelete != null)
            {
                Message[] messages = entityToDelete.Messages.ToArray();
                Array.ForEach(messages, this._messageRepository.Delete);
            }

            base.Delete(entity);
        }

        #endregion
    }
}