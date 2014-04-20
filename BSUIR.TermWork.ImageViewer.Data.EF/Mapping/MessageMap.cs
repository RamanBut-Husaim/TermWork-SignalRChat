using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    internal sealed class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.Text).IsRequired().HasMaxLength(Message.MaxLengthFor.Text);
            this.HasRequired(p => p.Friendship).WithMany(p => p.Messages).WillCascadeOnDelete(false);
            this.HasRequired(p => p.Sender).WithMany().WillCascadeOnDelete(false);
            this.HasRequired(p => p.Receiver).WithMany().WillCascadeOnDelete(false);
        }
    }
}
