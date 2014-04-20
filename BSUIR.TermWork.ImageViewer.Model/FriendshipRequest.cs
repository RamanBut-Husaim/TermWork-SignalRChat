using System;
using System.Data.SqlTypes;

namespace BSUIR.TermWork.ImageViewer.Model
{
    public sealed class FriendshipRequest : Entity<int>
    {
        private DateTime _creationdDate;
        private bool _isConfirmed;
        private Profile _sender;
        private Profile _receiver;

        public FriendshipRequest()
        {
        }

        public FriendshipRequest(Profile sender, Profile receiver)
        {
            this.Sender = sender;
            this.Receiver = receiver;
        }

        public DateTime CreationdDate
        {
            get { return this._creationdDate; }
            set { this._creationdDate = value; }
        }

        public bool IsConfirmed
        {
            get { return this._isConfirmed; }
            set { this._isConfirmed = value; }
        }

        public Profile Sender
        {
            get { return this._sender; }
            set { this._sender = value; }
        }

        public Profile Receiver
        {
            get { return this._receiver; }
            set { this._receiver = value; }
        }

        public static class MaxLengthFor
        {
            public static readonly DateTime MinSqlDateTime = SqlDateTime.MinValue.Value;
        }
    }
}
