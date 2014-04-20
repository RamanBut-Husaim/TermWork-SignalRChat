using System.Collections.Generic;

namespace BSUIR.TermWork.ImageViewer.Model
{
    public class User : Entity<int>
    {
        #region Fields

        private string _email;
        private ICollection<Role> _userRoles;

        #endregion

        #region Constructors and Destructors

        public User()
        {
        }

        public User(string email)
        {
            this._email = email;
            this._userRoles = new List<Role>();
        }

        #endregion

        #region Public Properties

        public string Email
        {
            get { return this._email; }
            private set { this._email = value; }
        }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public virtual Profile UserProfile { get; set; }

        public virtual ICollection<Role> UserRoles
        {
            get { return this._userRoles; }
            set { this._userRoles = value; }
        }

        #endregion

        #region Public Methods and Operators

        public override string ToString()
        {
            return string.Format("{0}", this._email);
        }

        #endregion

        public static class MaxLengthFor
        {
            #region Constants

            public const int Email = 70;

            public const string EmailValidation =
                @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

            public const int PasswordHash = 128;
            public const int PasswordMinimum = 8;
            public const int PasswordSalt = 128;

            #endregion
        }
    }
}