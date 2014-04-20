namespace BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Account
{
    using System.ComponentModel.DataAnnotations;

    using BSUIR.TermWork.ImageViewer.Model;

    public sealed class AccountSignInViewModel
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

        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "PasswordMissing")]
        [StringLength(User.MaxLengthFor.PasswordHash, MinimumLength = User.MaxLengthFor.PasswordMinimum,
            ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "InvalidPasswordLength")]
        public string Password
        {
            get;
            set;
        }

        #endregion
    }
}