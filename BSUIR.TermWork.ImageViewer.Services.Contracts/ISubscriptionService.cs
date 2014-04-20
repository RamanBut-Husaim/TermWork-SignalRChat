using System.Collections.Generic;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts
{
    public interface ISubscriptionService : IService
    {
        void AddSubscription(User subscriber, User target);

        bool VerifySubscriptionExistance(User subscriber, User target);

        IList<Subscription> GetFilteredSubscriptionsForUser(int key);

        IList<Subscription> GetSubscriptionsForUser(int key);

        void RemoveSubscription(User subscriber, User target);

        int CalculateNumberOfNewAlbums(User subscriber, User target);

        int CalculateNumberOfNewImages(User subscriber, User target);

        void ResetNewSubscriptions(User subscriber, User target);
    }
}
