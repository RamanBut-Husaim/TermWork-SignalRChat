﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace BSUIR.TermWork.ImageViewer.Model
{
    public sealed class Friendship : Entity<int>
    {
        private DateTime _creationDate;
        private Profile _firstProfile;
        private Profile _secondProfile;
        private ICollection<Message> _messages;

        public Friendship()
        {
        }

        public Friendship(Profile firstProfile, Profile secondProfile)
        {
            this.FirstProfile = firstProfile;
            this.SecondProfile = secondProfile;
        }

        public DateTime CreationDate
        {
            get { return this._creationDate; }
            set { this._creationDate = value; }
        }

        public Profile FirstProfile
        {
            get { return this._firstProfile; }
            set { this._firstProfile = value; }
        }

        public Profile SecondProfile
        {
            get { return this._secondProfile; }
            set { this._secondProfile = value; }
        }

        public ICollection<Message> Messages
        {
            get { return this._messages; }
            set { this._messages = value; }
        }

        public static class MaxLengthFor
        {
            public static readonly DateTime MinSqlDateTime = SqlDateTime.MinValue.Value;
        }
    }
}
