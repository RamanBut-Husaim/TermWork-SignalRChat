using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    using BSUIR.TermWork.ImageViewer.Model;
    using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Subscription;

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