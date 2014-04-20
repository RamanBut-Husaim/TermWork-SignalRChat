using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Search
{
    public class SearchResultViewModel
    {
        public int Key
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string OwnerName
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Reference
        {
            get;
            set;
        }

        public DateTime CreationDate
        {
            get;
            set;
        }
    }
}