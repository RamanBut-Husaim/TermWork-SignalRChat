namespace BSUIR.TermWork.ImageViewer.Model
{
    public class SubscriptionType : Entity<int>
    {
        public SubscriptionName Name { get; set; }

        public string Description { get; set; }

        public static class MaxLengthFor
        {
            public const int Description = 250;
        }
    }
}
