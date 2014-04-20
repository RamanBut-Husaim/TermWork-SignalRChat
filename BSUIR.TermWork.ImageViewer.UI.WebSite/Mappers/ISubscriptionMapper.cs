using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    using BSUIR.TermWork.ImageViewer.Model;
    using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Subscription;

    public interface ISubscriptionMapper
    {
        SubscriptionTargetViewModel BuildTarget(Subscription subscription, int itemNumber);
    }
}