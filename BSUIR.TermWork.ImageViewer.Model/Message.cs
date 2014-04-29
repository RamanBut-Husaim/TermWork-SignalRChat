using System;
using System.Data.SqlTypes;

namespace BSUIR.TermWork.ImageViewer.Model
{
    public class Message : Entity<int>
    {
        private string _text;
        private DateTime _sendDate;
        private Profile _sender;
        private Profile _receiver;
        private Friendship _friendship;

        public Message()
        {
        }

        public Message(string text)
        {
            this.Text = text;
        }

        public Message(Profile sender, Profile receicer)
        {
            this.Sender = sender;
            this.Receiver = receicer;
        }

        public Message(string text, Profile sender, Profile receiver) : this(sender, receiver)
        {
            this.Text = text;
        }

        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        public DateTime SendDate
        {
            get { return this._sendDate; }
            set { this._sendDate = value; }
        }

        public virtual Profile Sender
        {
            get { return this._sender; }
            set { this._sender = value; }
        }

        public virtual Profile Receiver
        {
            get { return this._receiver; }
            set { this._receiver = value; }
        }

        public virtual Friendship Friendship
        {
            get { return this._friendship; }
            set { this._friendship = value; }
        }

        public static class MaxLengthFor
        {
            public const int Text = 200;
            public static readonly DateTime MinSqlDateTime = SqlDateTime.MinValue.Value;
        }
    }
}
