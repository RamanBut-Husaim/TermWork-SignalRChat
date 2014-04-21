using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BSUIR.TermWork.ImageViewer.Data;
using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;

namespace BSUIR.TermWork.ImageViewer.Services
{
    public sealed class FriendshipService : ServiceBase, IFriendshipService
    {
        public FriendshipService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region Implementation of IFriendshipService

        public void SendRequest(User sender, User receiver)
        {
            throw new NotImplementedException();
        }

        public void ConfirmRequest(User sender, User receiver)
        {
            throw new NotImplementedException();
        }

        public void DeclineRequest(User sender, User receiver)
        {
            throw new NotImplementedException();
        }

        public void RemoveFriend(User sender, User receiver)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
