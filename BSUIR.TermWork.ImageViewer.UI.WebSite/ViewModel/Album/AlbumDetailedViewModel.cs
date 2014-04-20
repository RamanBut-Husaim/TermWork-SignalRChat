using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Album
{
    public class AlbumDetailedViewModel
    {
        public int Key
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public DateTime CreationDate
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public int ImageNumber
        {
            get;
            set;
        }
    }
}