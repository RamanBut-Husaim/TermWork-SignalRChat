using System;
using System.Collections.Generic;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data;
using BSUIR.TermWork.ImageViewer.Data.Exceptions;
using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Validators;

namespace BSUIR.TermWork.ImageViewer.Services
{
    public sealed class FriendshipService : ServiceBase, IFriendshipService
    {
        private readonly IEntityValidator<User> _userValidator; 

        public FriendshipService(
            IUnitOfWork unitOfWork, 
            IEntityValidator<User> userValidator) : base(unitOfWork)
        {
            this._userValidator = userValidator;
        }

        #region Implementation of IFriendshipService

        public void SendRequest(User sender, User receiver)
        {
            this._userValidator.Validate(sender);
            this._userValidator.Validate(receiver);
            try
            {
                var friendshipRequest = new FriendshipRequest(
                    sender.UserProfile, 
                    receiver.UserProfile);
                this.UnitOfWork.Repository<FriendshipRequest, int>().Insert(friendshipRequest);
                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new FriendshipServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        public void ConfirmRequest(User sender, User receiver)
        {
            this._userValidator.Validate(sender);
            this._userValidator.Validate(receiver);
            try
            {
                ICollection<FriendshipRequest> friendshipRequests =
                    sender.UserProfile.FriendshipRequests;
                FriendshipRequest request =
                    friendshipRequests.FirstOrDefault(p => p.Receiver.Key.Equals(receiver.Key));
                if (request != null && sender.Key != receiver.Key)
                {
                    var friendship = new Friendship(sender.UserProfile, receiver.UserProfile);
                    this.UnitOfWork.Repository<Friendship, int>().Insert(friendship);
                    request.IsConfirmed = true;
                    this.UnitOfWork.Repository<FriendshipRequest, int>().Update(request);
                    this.UnitOfWork.Save();
                }
            }
            catch (DbException ex)
            {
                throw new FriendshipServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        public void DeclineRequest(User sender, User receiver)
        {
            this._userValidator.Validate(sender);
            this._userValidator.Validate(receiver);
            try
            {
                ICollection<FriendshipRequest> friendshipRequests =
                    sender.UserProfile.FriendshipRequests;
                FriendshipRequest request =
                    friendshipRequests.FirstOrDefault(p => p.Receiver.Key.Equals(receiver.Key));
                if (request != null)
                {
                    request.IsDeclined = true;
                    this.UnitOfWork.Repository<FriendshipRequest, int>().Update(request);
                    this.UnitOfWork.Save();
                }
            }
            catch (DbException ex)
            {
                throw new FriendshipServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        public void RemoveFriend(User firstFriend, User secondFriend)
        {
            this._userValidator.Validate(firstFriend);
            this._userValidator.Validate(secondFriend);

            try
            {
                var friendshipToken =
                    this.UnitOfWork.Repository<Friendship, int>()
                        .Query()
                        .Filter(
                            p =>
                            (p.FirstProfile.Key == firstFriend.UserProfile.Key
                             || p.FirstProfile.Key == secondFriend.UserProfile.Key)
                            && (p.SecondProfile.Key == firstFriend.UserProfile.Key
                                || p.SecondProfile.Key == secondFriend.UserProfile.Key))
                        .Get()
                        .FirstOrDefault();
                if (friendshipToken != null)
                {
                    this.UnitOfWork.Repository<Friendship, int>().Delete(friendshipToken);
                    this.UnitOfWork.Save();
                }
            }
            catch (DbException ex)
            {
                throw new FriendshipServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        public IEnumerable<FriendshipRequest> GetUnconfirmedRequests(User receiver)
        {
            IEnumerable<FriendshipRequest> result;
            this._userValidator.Validate(receiver);
            try
            {
                result =
                    this.UnitOfWork.Repository<FriendshipRequest, int>()
                        .Query()
                        .Filter(
                            p =>
                            p.Receiver.Key.Equals(receiver.UserProfile.Key)
                            && p.IsConfirmed == false && p.IsDeclined == false)
                        .Get();
            }
            catch (DbException ex)
            {
                throw new FriendshipServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        public IEnumerable<Profile> GetFriends(User user)
        {
            this._userValidator.Validate(user);

            IList<Profile> result;

            try
            {
                List<Friendship> friendshipTokens =
                    this.UnitOfWork.Repository<Friendship, int>()
                        .Query()
                        .Filter(
                            p =>
                            p.FirstProfile.Key.Equals(user.UserProfile.Key)
                            || p.SecondProfile.Key.Equals(user.UserProfile.Key))
                        .Get()
                        .ToList();
                result = new List<Profile>(friendshipTokens.Count);
                foreach (var friendship in friendshipTokens)
                {
                    if (friendship.FirstProfile.User.Key != user.Key)
                    {
                        result.Add(friendship.FirstProfile);
                    }
                    else
                    {
                        result.Add(friendship.SecondProfile);
                    }
                }
            }
            catch (DbException ex)
            {
                throw new FriendshipServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        public IEnumerable<Profile> GetFriends(int userKey)
        {
            var user = this.UnitOfWork.Repository<User, int>().FindByKey(userKey);
            return this.GetFriends(user);
        }

        #endregion
    }
}
