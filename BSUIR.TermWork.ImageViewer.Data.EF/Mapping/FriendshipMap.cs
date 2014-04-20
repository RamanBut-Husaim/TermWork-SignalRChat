using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    internal sealed class FriendshipMap : EntityTypeConfiguration<Friendship>
    {
        public FriendshipMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.CreationDate).IsRequired();
            this.HasRequired(p => p.FirstProfile).WithMany().WillCascadeOnDelete(false);
            this.HasRequired(p => p.SecondProfile).WithMany().WillCascadeOnDelete(false);
            this.HasMany(p => p.Messages).WithRequired().WillCascadeOnDelete(false);
        }
    }
}
