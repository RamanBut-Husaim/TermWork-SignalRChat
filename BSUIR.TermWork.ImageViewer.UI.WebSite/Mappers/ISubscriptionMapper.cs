using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Subscription;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    public interface ISubscriptionMapper
    {
        SubscriptionTargetViewModel BuildTarget(Subscription subscription, int itemNumber);
    }
}