using System;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories
{
    public sealed class FriendshipRepository : Repository<Friendship, int>, IFriendshipRepository
    {
        private readonly IRepository<Message, int> _messageRepository;

        public FriendshipRepository(
            IDbContext context, 
            IRepository<Message, int> messageRepository)
            : base(context)
        {
            this._messageRepository = messageRepository;
        }

        #region Overrides of Repository<Friendship,int>

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
