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
    public sealed class MembershipService : ServiceBase, IMembershipService
    {
        private readonly IEntityValidator<User> _userValidator;
        private readonly IEntityValidator<Role> _roleValidator;
        private readonly IEntityValidator<Profile> _profileValidator;
        private readonly IEntityValidator<AccessRight> _accessRightValidator;
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IRepository<Role, int> _roleRepository;
        private readonly IRepository<AccessRight, int> _accessRightRepository;
        private readonly IHashGenerator _hashGenerator;

        public MembershipService(
            IUnitOfWork unitOfWork,
            IEntityValidator<User> userValidator,
            IEntityValidator<Role> roleValidator,
            IEntityValidator<Profile> profileValidator,
            IEntityValidator<AccessRight> accessRightValidator,
            IHashGenerator hashGenerator)
            : base(unitOfWork)
        {
            this._userValidator = userValidator;
            this._roleValidator = roleValidator;
            this._profileValidator = profileValidator;
            this._accessRightValidator = accessRightValidator;
            this._hashGenerator = hashGenerator;

            this._userRepository = this.UnitOfWork.Repository<User, int>() as IUserRepository;
            this._profileRepository =
                this.UnitOfWork.Repository<Profile, int>() as IProfileRepository;
            this._roleRepository = this.UnitOfWork.Repository<Role, int>();
            this._accessRightRepository = this.UnitOfWork.Repository<AccessRight, int>();
        }

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
                //	_profileRepository.Add(user.UserProfile);
                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }

        }

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

        public User SignInSocial(string email)
        {
            throw new NotImplementedException();
        }

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
                //	_profileRepository[user.UserProfile.Key] = user.UserProfile;
                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new MembershipServiceException(EntityValidationException.DefaultMessage, ex);
            }

        }

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
    }
}
