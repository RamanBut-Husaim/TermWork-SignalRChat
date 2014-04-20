using System.Collections.Generic;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts
{
    public interface IMembershipService : IService
    {
        #region Public Methods and Operators

        void CreateUser(User user);

        IList<User> GetAllUsers();

        IList<Role> GetRoles();

        User GetUserByEmail(string email);

        User GetUserByKey(int key);

        IList<User> GetUsersByFirstName(string firstName);

        IList<User> GetUsersByLastName(string lastName);

        void RemoveUser(int key);

        void RemoveUser(User user);

        User SignIn(string email, string password);

        User SignInSocial(string email);

        void SignOut(int key);

        void SignOut(User user);

        void UpdateUser(User user);

        bool UserExists(string email);

        bool UserExists(int key);

        bool UserExists(User user);

        void AddRoleToUser(User user, RoleName roleName);

        void RemoveRoleFromUser(User user, RoleName roleName);

        #endregion
    }
}