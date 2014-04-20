using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Image
{
    public class ImageDetailedViewModel
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

        public string AlbumName
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public DateTime UploadDate
        {
            get;
            set;
        }

        public int Rate
        {
            get;
            set;
        }
    }
}