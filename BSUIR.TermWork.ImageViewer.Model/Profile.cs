using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace BSUIR.TermWork.ImageViewer.Model
{
    public class Profile : Entity<int>, IEquatable<Profile>
    {
        #region Fields

        private string _firstName;
        private bool _isSignedIn;
        private string _lastName;
        private DateTime _lastSignIn;
        private DateTime _lastSignOut;
        private DateTime _registrationDate;
        private ICollection<Subscription> _subscriptions;
        private ICollection<FriendshipRequest> _friendshipRequests;
        private User _user;

        #endregion

        #region Constructors and Destructors

        public Profile()
        {
        }

        public Profile(string firstName, string lastName)
        {
            this._firstName = firstName;
            this._lastName = lastName;
            this._registrationDate = DateTime.Now;
            this._lastSignIn = DateTime.Now;
            this._lastSignOut = DateTime.Now;
        }

        public Profile(User user)
        {
            this._user = user;
        }

        #endregion

        #region Public Properties

        public string FirstName
        {
            get { return this._firstName; }
            set { this._firstName = value; }
        }

        public bool IsSignedIn
        {
            get { return this._isSignedIn; }
            set { this._isSignedIn = value; }
        }

        public string LastName
        {
            get { return this._lastName; }
            set { this._lastName = value; }
        }

        public DateTime LastSignIn
        {
            get { return this._lastSignIn; }
            set { this._lastSignIn = value; }
        }

        public DateTime LastSignOut
        {
            get { return this._lastSignOut; }
            set { this._lastSignOut = value; }
        }

        public DateTime RegistrationDate
        {
            get { return this._registrationDate; }
            set { this._registrationDate = value; }
        }

        public virtual ICollection<Subscription> Subscriptions
        {
            get { return this._subscriptions; }
            set { this._subscriptions = value; }
        }

        public virtual User User
        {
            get { return this._user; }
            set { this._user = value; }
        }

        public virtual ICollection<FriendshipRequest> FriendshipRequests
        {
            get { return this._friendshipRequests; }
            set { this._friendshipRequests = value; }
        }

        #endregion

        #region Public Methods and Operators

        public bool Equals(Profile other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (!this.Key.Equals(other.Key))
            {
                return false;
            }

            if (!this.FirstName.Equals(other.FirstName))
            {
                return false;
            }

            if (!this.LastName.Equals(other.LastName))
            {
                return false;
            }

            return true;
        }

        #region Overrides of Object

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals(obj as Profile);
        }

        #region Overrides of Object

        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }

        #endregion

        #endregion

        public override string ToString()
        {
            return string.Format("{0} {1}", this._firstName, this._lastName);
        }

        #endregion

        public static class MaxLengthFor
        {
            #region Constants

            public const int FirstName = 50;
            public const int LastName = 50;
            public static readonly DateTime MinDate = SqlDateTime.MinValue.Value;

            #endregion
        }
    }
}