using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    internal sealed class SubscriptionTypeMap : EntityTypeConfiguration<SubscriptionType>
    {
        public SubscriptionTypeMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.Description)
                .HasMaxLength(SubscriptionType.MaxLengthFor.Description);
            this.Property(p => p.Name).IsRequired();
        }
    }
}
