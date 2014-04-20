using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Comment
{
    using System.ComponentModel.DataAnnotations;

    using BSUIR.TermWork.ImageViewer.Model;

    public class CommentCreateViewModel
    {
        public int Key
        {
            get;
            set;
        }

        public DateTime CreationDate
        {
            get;
            set;
        }
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "Exception_EmptyComment")]
        [StringLength(Album.MaxLengthFor.Name, ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "Exception_CommentTooLong")]
        public string Content
        {
            get;
            set;
        }
    }
}