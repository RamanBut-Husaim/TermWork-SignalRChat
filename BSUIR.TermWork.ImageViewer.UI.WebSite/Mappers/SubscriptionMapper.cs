using System;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Subscription;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    public sealed class SubscriptionMapper : ISubscriptionMapper
    {
        public SubscriptionTargetViewModel BuildTarget(Subscription subscription, int itemNumber)
        {
            if (subscription == null)
            {
                throw new ArgumentNullException("subscription");
            }
            var result = new SubscriptionTargetViewModel();
            result.FirstName = subscription.Target.FirstName;
            result.LastName = subscription.Target.LastName;
            result.Key = subscription.Target.User.Key;
            result.NewSubscriptionItems = itemNumber;
            return result;
        }
    }
}