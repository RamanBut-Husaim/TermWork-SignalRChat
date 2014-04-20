using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Account
{
    using System.ComponentModel.DataAnnotations;

    using BSUIR.TermWork.ImageViewer.Model;

    public class AccountEditViewModel
    {
        public int Key
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "FirstNameMissing")]
        [StringLength(Profile.MaxLengthFor.FirstName, ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "InvalidFirstNameLength")]
        public string FirstName
        {
            get;
            set;
        }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "LastNameMissing")]
        [StringLength(Profile.MaxLengthFor.LastName, ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "InvalidLastNameLength")]
        public string LastName
        {
            get;
            set;
        }

        public DateTime RegistrationDate
        {
            get;
            set;
        }
    }
}