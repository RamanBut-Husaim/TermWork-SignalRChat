// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MembershipService.cs" company="">
//   
// </copyright>
// <summary>
//   The membership service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data;
using BSUIR.TermWork.ImageViewer.Data.Exceptions;
using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Validators;
using BSUIR.TermWork.ImageViewer.Shared;

namespace BSUIR.TermWork.ImageViewer.Services
{
    /// <summary>
    /// The membership service.
    /// </summary>
    public sealed class MembershipService : ServiceBase, IMembershipService
    {
        #region Fields

        /// <summary>
        /// The _hash generator.
        /// </summary>
        private readonly IHashGenerator _hashGenerator;

        /// <summary>
        /// The _profile repository.
        /// </summary>
        private readonly IProfileRepository _profileRepository;

        /// <summary>
        /// The _profile validator.
        /// </summary>
        private readonly IEntityValidator<Profile> _profileValidator;

        /// <summary>
        /// The _role repository.
        /// </summary>
        private readonly IRepository<Role, int> _roleRepository;

        /// <summary>
        /// The _role validator.
        /// </summary>
        private readonly IEntityValidator<Role> _roleValidator;

        /// <summary>
        /// The _user repository.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// The _user validator.
        /// </summary>
        private readonly IEntityValidator<User> _userValidator;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipService"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        /// <param name="userValidator">
        /// The user validator.
        /// </param>
        /// <param name="roleValidator">
        /// The role validator.
        /// </param>
        /// <param name="profileValidator">
        /// The profile validator.
        /// </param>
        /// <param name="accessRightValidator">
        /// The access right validator.
        /// </param>
        /// <param name="hashGenerator">
        /// The hash generator.
        /// </param>
        public MembershipService(
            IUnitOfWork unitOfWork, 
            IEntityValidator<User> userValidator, 
            IEntityValidator<Role> roleValidator, 
            IEntityValidator<Profile> profileValidator, 
            IHashGenerator hashGenerator) : base(unitOfWork)
        {
            this._userValidator = userValidator;
            this._roleValidator = roleValidator;
            this._profileValidator = profileValidator;
            this._hashGenerator = hashGenerator;

            this._userRepository = this.UnitOfWork.Repository<User, int>() as IUserRepository;
            this._profileRepository =
                this.UnitOfWork.Repository<Profile, int>() as IProfileRepository;
            this._roleRepository = this.UnitOfWork.Repository<Role, int>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add role to user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public void AddRoleToUser(User user, RoleName roleName)
        {
            this._userValidator.Validate(user);
            try
            {
                User tempUser = this.UnitOfWork.Repository<User, int>().FindByKey(user.Key);
                if (!tempUser.UserRoles.Any(p => p.Name == roleName))
                {
                    Role role =
                        this.UnitOfWork.Repository<Role, int>()
                            .Query()
                            .Filter(p => p.Name == roleName)
                            .Get()
                            .FirstOrDefault();
                    if (role != null)
                    {
                        tempUser.UserRoles.Add(role);
                        this.UnitOfWork.Save();
                    }
                }
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        /// <summary>
        /// The create user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <exception cref="DuplicateEmailException">
        /// </exception>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public void CreateUser(User user)
        {
            this._userValidator.Validate(user);
            this._profileValidator.Validate(user.UserProfile);

            try
            {
                if (
                    this._userRepository.Query()
                        .Filter(p => user.Email.Equals(p.Email))
                        .Get()
                        .FirstOrDefault() != null)
                {
                    throw new DuplicateEmailException(
                        "The user with email " + user.Email + " already exists.");
                }

                user.UserProfile.LastSignIn = DateTime.Now;
                user.UserProfile.LastSignOut = DateTime.Now;
                Role role =
                    this._roleRepository.Query()
                        .Include(p => p.AccessRights)
                        .Get()
                        .First(p => p.Name.Equals(RoleName.RegisteredUser));
                user.UserRoles.Add(role);
                this._userRepository.Insert(user);

                // 	_profileRepository.Add(user.UserProfile);
                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        /// <summary>
        /// The get all users.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public IList<User> GetAllUsers()
        {
            IList<User> resultUsers;
            try
            {
                resultUsers = this._userRepository.Query().Get().ToList();
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return resultUsers;
        }

        /// <summary>
        /// The get roles.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public IList<Role> GetRoles()
        {
            IList<Role> resultRoles;
            try
            {
                resultRoles = this._roleRepository.Query().Get().ToList();
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return resultRoles;
        }

        /// <summary>
        /// The get user by email.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public User GetUserByEmail(string email)
        {
            this._userValidator.ValidateProperty(p => p.Email, email);
            User resultUser;
            try
            {
                resultUser =
                    this._userRepository.Query()
                        .Filter(p => email.Equals(p.Email))
                        .Include(p => p.UserRoles)
                        .Get()
                        .FirstOrDefault();
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return resultUser;
        }

        /// <summary>
        /// The get user by key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public User GetUserByKey(int key)
        {
            this._userValidator.ValidateProperty(p => p.Key, key);
            User resultUser;
            try
            {
                resultUser = this._userRepository.FindByKey(key);
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return resultUser;
        }

        /// <summary>
        /// The get users by first name.
        /// </summary>
        /// <param name="firstName">
        /// The first name.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public IList<User> GetUsersByFirstName(string firstName)
        {
            this._profileValidator.ValidateProperty(p => p.FirstName, firstName);
            IList<User> resultUsers;
            try
            {
                resultUsers =
                    this._userRepository.Query()
                        .Filter(p => firstName.Equals(p.UserProfile.FirstName))
                        .Get()
                        .ToList();
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return resultUsers;
        }

        /// <summary>
        /// The get users by last name.
        /// </summary>
        /// <param name="lastName">
        /// The last name.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public IList<User> GetUsersByLastName(string lastName)
        {
            this._profileValidator.ValidateProperty(p => p.FirstName, lastName);
            IList<User> resultUsers;
            try
            {
                resultUsers =
                    this._userRepository.Query()
                        .Filter(p => lastName.Equals(p.UserProfile.LastName))
                        .Get()
                        .ToList();
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return resultUsers;
        }

        /// <summary>
        /// The remove role from user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public void RemoveRoleFromUser(User user, RoleName roleName)
        {
            this._userValidator.Validate(user);
            try
            {
                User tempUser = this.UnitOfWork.Repository<User, int>().FindByKey(user.Key);
                if (tempUser.UserRoles.Any(p => p.Name == roleName))
                {
                    Role role = tempUser.UserRoles.FirstOrDefault(p => p.Name == roleName);
                    if (role != null)
                    {
                        tempUser.UserRoles.Remove(role);
                        this.UnitOfWork.Save();
                    }
                }
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        /// <summary>
        /// The remove user.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <exception cref="UserNotFoundException">
        /// </exception>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public void RemoveUser(int key)
        {
            this._userValidator.ValidateProperty(p => p.Key, key);
            try
            {
                User temp = this.GetUserByKey(key);
                if (temp != null)
                {
                    this._userRepository.Delete(temp);
                }
                else
                {
                    throw new UserNotFoundException("There is no such user in the system.");
                }

                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        /// <summary>
        /// The remove user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <exception cref="UserNotFoundException">
        /// </exception>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public void RemoveUser(User user)
        {
            this._userValidator.Validate(user);
            try
            {
                User temp = this.GetUserByKey(user.Key);
                if (temp != null)
                {
                    this._userRepository.Delete(temp);
                }
                else
                {
                    throw new UserNotFoundException("There is no such user in the system.");
                }

                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        /// <summary>
        /// The sign in.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        /// <exception cref="UserNotFoundException">
        /// </exception>
        /// <exception cref="AuthenticationException">
        /// </exception>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public User SignIn(string email, string password)
        {
            this._userValidator.ValidateProperty(p => p.Email, email);
            this._userValidator.ValidateProperty(p => p.PasswordHash, password);
            User resultUser;
            try
            {
                resultUser = this.GetUserByEmail(email);
                if (resultUser == null)
                {
                    throw new UserNotFoundException("There is no such user in the system.");
                }

                string targetPassword = this._hashGenerator.GetPasswordHash(
                    password, 
                    resultUser.PasswordSalt);
                if (!resultUser.PasswordHash.Equals(targetPassword))
                {
                    throw new AuthenticationException("Invalid user name or password.");
                }

                resultUser.UserProfile.LastSignIn = DateTime.Now;
                this._profileRepository[resultUser.UserProfile.Key] = resultUser.UserProfile;
                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return resultUser;
        }

        /// <summary>
        /// The sign in social.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public User SignInSocial(string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The sign out.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <exception cref="UserNotFoundException">
        /// </exception>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public void SignOut(int key)
        {
            this._userValidator.ValidateProperty(p => p.Key, key);
            try
            {
                User repoUser = this.GetUserByKey(key);
                if (repoUser == null)
                {
                    throw new UserNotFoundException("There is no such user in the system.");
                }

                repoUser.UserProfile.LastSignOut = DateTime.Now;
                repoUser.UserProfile.IsSignedIn = false;
                this._profileRepository[repoUser.UserProfile.Key] = repoUser.UserProfile;
                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        /// <summary>
        /// The sign out.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <exception cref="UserNotFoundException">
        /// </exception>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public void SignOut(User user)
        {
            this._userValidator.Validate(user);
            try
            {
                User repoUser = this.GetUserByKey(user.Key);
                if (repoUser == null)
                {
                    throw new UserNotFoundException("There is no such user in the system.");
                }

                repoUser.UserProfile.LastSignOut = DateTime.Now;
                repoUser.UserProfile.IsSignedIn = false;
                this._profileRepository[repoUser.UserProfile.Key] = repoUser.UserProfile;
                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        /// <summary>
        /// The update user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public void UpdateUser(User user)
        {
            this._userValidator.Validate(user);
            try
            {
                User sourceUser = this.GetUserByKey(user.Key);
                sourceUser.UserProfile.FirstName = user.UserProfile.FirstName;
                sourceUser.UserProfile.LastName = user.UserProfile.LastName;
                this._profileRepository.Update(user.UserProfile);
                this._userRepository.Update(user);

                // 	_profileRepository[user.UserProfile.Key] = user.UserProfile;
                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        /// <summary>
        /// The user exists.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public bool UserExists(string email)
        {
            bool result;
            this._userValidator.ValidateProperty(p => p.Email, email);
            try
            {
                User temp = this.GetUserByEmail(email);
                result = temp == null;
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// The user exists.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public bool UserExists(int key)
        {
            bool result;
            this._userValidator.ValidateProperty(p => p.Key, key);
            try
            {
                User temp = this.GetUserByKey(key);
                result = temp == null;
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// The user exists.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="MembershipServiceException">
        /// </exception>
        public bool UserExists(User user)
        {
            this._userValidator.Validate(user);
            bool result = false;
            try
            {
                User temp = this.GetUserByKey(user.Key);
                result = temp == null;
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        #endregion
    }
}