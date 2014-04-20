using System;

namespace BSUIR.TermWork.ImageViewer.Model
{
    public class Subscription : Entity<int>, IEquatable<Subscription>
    {
        #region Fields

        private DateTime _creationDate;

        #endregion

        #region Constructors and Destructors

        #endregion

        #region Public Properties

        public virtual Profile Subscriber { get; set; }

        public virtual Profile Target { get; set; }

        public virtual SubscriptionType Type { get; set; }

        public DateTime CreationDate
        {
            get { return this._creationDate; }
            set { this._creationDate = value; }
        }

        public static class MaxLenghtFor
        {
            public const int Content = 100;
        }

        #endregion

        #region Implementation of IEquatable<Subscription>

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Subscription other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (!this.Key.Equals(other.Key))
            {
                return false;
            }

            if (!this.Subscriber.Equals(other.Subscriber))
            {
                return false;
            }

            if (!this.Target.Equals(other.Target))
            {
                return false;
            }

            return true;
        }

        #region Overrides of Object

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
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
            return this.Equals(obj as Subscription);
        }

        #region Overrides of Object

        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }

        #endregion

        #endregion

        #endregion
    }
}