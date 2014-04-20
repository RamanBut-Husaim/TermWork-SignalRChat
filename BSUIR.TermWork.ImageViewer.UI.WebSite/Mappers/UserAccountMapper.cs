namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    using System;
    using System.Linq;

    using BSUIR.TermWork.ImageViewer.Model;
    using BSUIR.TermWork.ImageViewer.Shared;
    using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Account;

    public sealed class UserAccountMapper : IUserAccountMapper
    {
        private readonly IHashGenerator _hashGenerator;

        public UserAccountMapper(IHashGenerator hashGenerator)
        {
            this._hashGenerator = hashGenerator;
        }

        public AccountEditViewModel BuildEdit(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var result = new AccountEditViewModel();
            result.FirstName = user.UserProfile.FirstName;
            result.LastName = user.UserProfile.LastName;
            result.Email = user.Email;
            result.Key = user.Key;
            result.RegistrationDate = user.UserProfile.RegistrationDate;
            return result;
        }

        public AccountSimpleViewModel BuildSimple(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var result = new AccountSimpleViewModel();
            result.Key = user.Key;
            result.Email = user.Email;
            return result;
        }

        public AccountAdminListViewModel BuildAdminList(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var result = new AccountAdminListViewModel();
            result.Key = user.Key;
            result.FirstName = user.UserProfile.FirstName;
            result.LastName = user.UserProfile.LastName;
            result.RegistrationDate = user.UserProfile.RegistrationDate;
            result.Email = user.Email;
            result.Roles = user.UserRoles.Select(p => p.Name.ToString()).ToArray();
            return result;
        }

        public User BuildRegister(AccountRegisterViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }
            if (!viewModel.Password.Equals(viewModel.PasswordConfirmation))
            {
                throw new ArgumentException("The passwords are not equal.");
            }
            var resultUser = new User(viewModel.Email);
            var resultProfile = new Profile(resultUser);
            resultUser.UserProfile = resultProfile;
            resultProfile.FirstName = viewModel.FirstName;
            resultProfile.LastName = viewModel.LastName;
            resultProfile.RegistrationDate = DateTime.UtcNow;
            resultProfile.LastSignIn = DateTime.UtcNow;
            resultProfile.LastSignOut = DateTime.UtcNow;
            resultProfile.IsSignedIn = true;
            resultUser.PasswordSalt = this._hashGenerator.GenerateSalt(User.MaxLengthFor.PasswordSalt);
            resultUser.PasswordHash = this._hashGenerator.GetPasswordHash(viewModel.Password, resultUser.PasswordSalt);
            return resultUser;
        }

        public AccountRegisterViewModel BuildUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var resultViewModel = new AccountRegisterViewModel();
            resultViewModel.Email = user.Email;
            if (user.UserProfile != null)
            {
                resultViewModel.FirstName = user.UserProfile.FirstName;
                resultViewModel.LastName = user.UserProfile.LastName;
            }
            return resultViewModel;
        }

        public AccountInfoViewModel BuildInfo(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var result = new AccountInfoViewModel();
            result.FirstName = user.UserProfile.FirstName;
            result.LastName = user.UserProfile.LastName;
            result.Key = user.Key;
            result.RegistrationDate = user.UserProfile.RegistrationDate;
            result.Email = user.Email;
            return result;
        }

        public void UpdateUser(User user, AccountEditViewModel viewModel)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }
            user.UserProfile.FirstName = viewModel.FirstName;
            user.UserProfile.LastName = viewModel.LastName;
        }

        public void UpdateUser(User user, AccountInfoViewModel viewModel)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }
            user.UserProfile.FirstName = viewModel.FirstName;
            user.UserProfile.LastName = viewModel.LastName;
        }
    }
}