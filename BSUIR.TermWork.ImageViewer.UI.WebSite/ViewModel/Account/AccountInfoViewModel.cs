namespace BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BSUIR.TermWork.ImageViewer.Model;

    public sealed class AccountInfoViewModel
    {
        #region Public Properties

        public string Email
        {
            get;
            set;
        }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "FirstNameMissing")
        ]
        [StringLength(Profile.MaxLengthFor.FirstName, ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "InvalidFirstNameLength")]
        public string FirstName
        {
            get;
            set;
        }

        public int Key
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

        #endregion
    }
}