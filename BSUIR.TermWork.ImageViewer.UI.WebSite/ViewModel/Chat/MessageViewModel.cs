using System;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Chat
{
    public sealed class MessageViewModel
    {
        public string Sender { get; set; }

        public string Author { get; set; }

        public DateTime? Date { get; set; }

        public string Text { get; set; }
    }
}