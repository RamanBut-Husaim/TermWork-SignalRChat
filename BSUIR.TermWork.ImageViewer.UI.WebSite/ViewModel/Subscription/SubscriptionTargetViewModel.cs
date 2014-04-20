using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Subscription
{
    public class SubscriptionTargetViewModel
    {
        //target user key
        public int Key
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public int NewSubscriptionItems
        {
            get;
            set;
        }
    }
}