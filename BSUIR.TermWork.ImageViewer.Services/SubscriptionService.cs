using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data;
using BSUIR.TermWork.ImageViewer.Data.Exceptions;
using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Validators;

namespace BSUIR.TermWork.ImageViewer.Services
{
    public sealed class SubscriptionService : ServiceBase, ISubscriptionService
    {
        private readonly IEntityValidator<User> _userValidator;

        public SubscriptionService(IUnitOfWork unitOfWork, IEntityValidator<User> userValidator)
            : base(unitOfWork)
        {
            this._userValidator = userValidator;
        }

        public void AddSubscription(User subscriber, User target)
        {
            this._userValidator.Validate(subscriber);
            this._userValidator.Validate(target);

            try
            {
                User tempSubscriber =
                    this.UnitOfWork.Repository<User, int>().FindByKey(subscriber.Key);
                User tempTarget = this.UnitOfWork.Repository<User, int>().FindByKey(target.Key);
                if (!this.VerifyExistance(tempSubscriber, tempTarget))
                {
                    var albumSubscription = new Subscription();
                    albumSubscription.Subscriber = tempSubscriber.UserProfile;
                    albumSubscription.Target = tempTarget.UserProfile;
                    albumSubscription.CreationDate = DateTime.UtcNow;
                    albumSubscription.Type =
                        this.UnitOfWork.Repository<SubscriptionType, int>()
                            .Query()
                            .Filter(p => p.Name == SubscriptionName.Album)
                            .Get()
                            .FirstOrDefault();
                    var imageSubscription = new Subscription();
                    imageSubscription.Subscriber = tempSubscriber.UserProfile;
                    imageSubscription.Target = tempTarget.UserProfile;
                    imageSubscription.CreationDate = DateTime.UtcNow;
                    imageSubscription.Type =
                        this.UnitOfWork.Repository<SubscriptionType, int>()
                            .Query()
                            .Filter(p => p.Name == SubscriptionName.Image)
                            .Get()
                            .FirstOrDefault();
                    if (tempSubscriber.UserProfile.Subscriptions == null)
                    {
                        tempSubscriber.UserProfile.Subscriptions = new Collection<Subscription>();
                    }

                    tempSubscriber.UserProfile.Subscriptions.Add(albumSubscription);
                    tempSubscriber.UserProfile.Subscriptions.Add(imageSubscription);
                    this.UnitOfWork.Repository<Subscription, int>().Insert(albumSubscription);
                    this.UnitOfWork.Repository<Subscription, int>().Insert(imageSubscription);
                    this.UnitOfWork.Repository<Profile, int>().Update(tempSubscriber.UserProfile);
                    this.UnitOfWork.Save();
                }
            }
            catch (DbException ex)
            {
                throw new SubscriptionServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        public void RemoveSubscription(User subscriber, User target)
        {
            this._userValidator.Validate(subscriber);
            this._userValidator.Validate(target);

            try
            {
                User tempSubscriber =
                    this.UnitOfWork.Repository<User, int>().FindByKey(subscriber.Key);
                User tempTarget = this.UnitOfWork.Repository<User, int>().FindByKey(target.Key);
                if (this.VerifyExistance(tempSubscriber, tempTarget))
                {
                    this.RemoveMatchedSubscriptions(tempSubscriber, tempTarget);
                }
                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new SubscriptionServiceException(EntityValidationException.DefaultMessage, ex);
            }

        }

        public int CalculateNumberOfNewAlbums(User subscriber, User target)
        {
            this._userValidator.Validate(subscriber);
            this._userValidator.Validate(target);
            int result = 0;
            try
            {
                User tempSubscriber =
                    this.UnitOfWork.Repository<User, int>().FindByKey(subscriber.Key);
                User tempTarget = this.UnitOfWork.Repository<User, int>().FindByKey(target.Key);
                Subscription subscriptionTargets = this.GetSubscription(
                    SubscriptionName.Album,
                    tempSubscriber,
                    tempTarget);
                if (subscriptionTargets != null)
                {
                    result +=
                        this.UnitOfWork.Repository<Album, int>()
                            .Query()
                            .Filter(p => p.CreationDate >= subscriptionTargets.CreationDate)
                            .Get()
                            .Count();
                }
            }
            catch (DbException ex)
            {
                throw new SubscriptionServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        public int CalculateNumberOfNewImages(User subscriber, User target)
        {
            this._userValidator.Validate(subscriber);
            this._userValidator.Validate(target);
            int result = 0;
            try
            {
                User tempSubscriber =
                    this.UnitOfWork.Repository<User, int>().FindByKey(subscriber.Key);
                User tempTarget = this.UnitOfWork.Repository<User, int>().FindByKey(target.Key);
                Subscription subscriptionTargets = this.GetSubscription(
                    SubscriptionName.Image,
                    tempSubscriber,
                    tempTarget);
                if (subscriptionTargets != null)
                {
                    result +=
                        this.UnitOfWork.Repository<Image, int>()
                            .Query()
                            .Filter(p => p.UploadDate >= subscriptionTargets.CreationDate)
                            .Get()
                            .Count();
                }
            }
            catch (DbException ex)
            {
                throw new SubscriptionServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        private Subscription GetSubscription(SubscriptionName name, User subscriber, User target)
        {
            Subscription result =
                subscriber.UserProfile.Subscriptions.FirstOrDefault(
                    p => p.Target.Key.Equals(target.Key) && p.Type.Name.Equals(name));

            return result;
        }

        private void RemoveMatchedSubscriptions(User subscriber, User target)
        {
            IList<Subscription> matches =
                subscriber.UserProfile.Subscriptions.Where(p => p.Target.Key.Equals(target.Key))
                          .ToList();
            for (int i = 0; i < matches.Count; ++i)
            {
                subscriber.UserProfile.Subscriptions.Remove(matches[i]);
                this.UnitOfWork.Repository<Subscription, int>().Delete(matches[i]);
            }
        }

        public bool VerifySubscriptionExistance(User subscriber, User target)
        {
            this._userValidator.Validate(subscriber);
            this._userValidator.Validate(target);

            bool result;

            try
            {
                User tempSubscriber =
                    this.UnitOfWork.Repository<User, int>().FindByKey(subscriber.Key);
                User tempTarget = this.UnitOfWork.Repository<User, int>().FindByKey(target.Key);
                result = this.VerifyExistance(tempSubscriber, tempTarget);
            }
            catch (DbException ex)
            {
                throw new SubscriptionServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        public void ResetNewSubscriptions(User subscriber, User target)
        {
            this._userValidator.Validate(subscriber);
            this._userValidator.Validate(target);

            try
            {
                User tempSubscriber =
                    this.UnitOfWork.Repository<User, int>().FindByKey(subscriber.Key);
                User tempTarget = this.UnitOfWork.Repository<User, int>().FindByKey(target.Key);
                IList<Subscription> subscriptions =
                    tempSubscriber.UserProfile.Subscriptions.Where(
                        p => p.Target.Key.Equals(tempTarget.Key)).ToList();
                for (int i = 0; i < subscriptions.Count; ++i)
                {
                    subscriptions[i].CreationDate = DateTime.UtcNow;
                    this.UnitOfWork.Repository<Subscription, int>().Update(subscriptions[i]);
                }
                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new SubscriptionServiceException(EntityValidationException.DefaultMessage, ex);
            }

        }

        private bool VerifyExistance(User subscriber, User target)
        {
            bool result = false;
            if (subscriber.UserProfile.Subscriptions != null)
            {
                Subscription match =
                    subscriber.UserProfile.Subscriptions.FirstOrDefault(
                        p => p.Target.User.Key.Equals(target.Key));
                if (match != null)
                {
                    result = true;
                }
            }

            return result;
        }

        public IList<Subscription> GetFilteredSubscriptionsForUser(int key)
        {
            IList<Subscription> result = null;
            try
            {
                User owner = this.UnitOfWork.Repository<User, int>().FindByKey(key);
                if (owner != null)
                {
                    result = owner.UserProfile.Subscriptions.ToList();
                    result = this.FilterSubscriptions(result);
                }
            }
            catch (DbException ex)
            {
                throw new SubscriptionServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        public IList<Subscription> GetSubscriptionsForUser(int key)
        {
            IList<Subscription> result = null;
            try
            {
                User owner = this.UnitOfWork.Repository<User, int>().FindByKey(key);
                if (owner != null)
                {
                    result = owner.UserProfile.Subscriptions.ToList();
                }
            }
            catch (DbException ex)
            {
                throw new SubscriptionServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        private IList<Subscription> FilterSubscriptions(IList<Subscription> subscriptions)
        {
            IList<Subscription> result = new List<Subscription>();
            for (int i = 0; i < subscriptions.Count; ++i)
            {
                int i1 = i;
                if (!result.Any(p => p.Target.Key.Equals(subscriptions[i1].Target.Key)))
                {
                    result.Add(subscriptions[i]);
                }
            }
            return result;
        }
    }
}
