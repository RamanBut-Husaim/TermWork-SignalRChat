namespace BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Account
{
    using System.ComponentModel.DataAnnotations;

    using BSUIR.TermWork.ImageViewer.Model;

    public sealed class AccountRegisterViewModel
    {
        #region Public Properties

        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "EmptyEmail")]
        [RegularExpression(User.MaxLengthFor.EmailValidation, ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "InvalidEmail")]
        [StringLength(User.MaxLengthFor.Email, ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "EmailTooLong")]
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

        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "PasswordMissing")]
        [StringLength(User.MaxLengthFor.PasswordHash, MinimumLength = User.MaxLengthFor.PasswordMinimum,
            ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "InvalidPasswordLength")]
        [Compare("PasswordConfirmation", ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "Account_DifferentPassowrds")]
        public string Password
        {
            get;
            set;
        }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "PasswordConfirmationMissing")]
        [StringLength(User.MaxLengthFor.PasswordHash, MinimumLength = User.MaxLengthFor.PasswordMinimum,
            ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "InvalidPasswordLength")]
        public string PasswordConfirmation
        {
            get;
            set;
        }

        #endregion
    }
}