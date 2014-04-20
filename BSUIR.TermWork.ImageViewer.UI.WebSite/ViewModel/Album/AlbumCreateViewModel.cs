using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Album
{
    using System.ComponentModel.DataAnnotations;

    using BSUIR.TermWork.ImageViewer.Model;

    public sealed class AlbumCreateViewModel
    {
        public int Key
        {
            get;
            set;
        }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "Exception_EmptyAlbumName")]
        [StringLength(Album.MaxLengthFor.Name, ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "Exception_AlbumNameTooLong")]
        public string Name
        {
            get;
            set;
        }

        [StringLength(Album.MaxLengthFor.Description, ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "Exception_DescriptionTooLong")]
        public string Description
        {
            get;
            set;
        }
    }
}