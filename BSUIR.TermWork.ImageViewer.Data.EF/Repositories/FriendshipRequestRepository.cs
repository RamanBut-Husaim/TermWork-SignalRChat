using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Repositories
{
    public sealed class FriendshipRequestRepository : Repository<FriendshipRequest, int>, IFriendshipRequestRepository
    {
        public FriendshipRequestRepository(IDbContext context) : base(context)
        {
        }

        #region Overrides of Repository<FriendshipRequest,int>

        public override void Insert(FriendshipRequest entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity cannot be null");
            }

            FriendshipRequest duplicate =
                this.Query()
                    .Filter(
                        p =>
                        p.Sender.Key.Equals(entity.Sender.Key)
                        && p.Receiver.Key.Equals(entity.Receiver.Key))
                    .Get()
                    .FirstOrDefault();
            if (duplicate == null)
            {
                base.Insert(entity);
            }
        }

        #endregion
    }
}
