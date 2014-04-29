using System;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Friendship
{
    public sealed class FriendshipRequestViewModel
    {
        public int? UserKey { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime RequestDate { get; set; }
    }
}