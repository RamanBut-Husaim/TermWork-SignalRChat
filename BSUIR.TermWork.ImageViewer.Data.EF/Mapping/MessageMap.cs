// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageMap.cs" company="">
//   
// </copyright>
// <summary>
//   The message map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    /// <summary>
    /// The message map.
    /// </summary>
    internal sealed class MessageMap : EntityTypeConfiguration<Message>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageMap"/> class.
        /// </summary>
        public MessageMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.Text).IsRequired().HasMaxLength(Message.MaxLengthFor.Text);
            this.HasRequired(p => p.Friendship).WithMany(p => p.Messages).WillCascadeOnDelete(false);
            this.HasRequired(p => p.Sender).WithMany().WillCascadeOnDelete(false);
            this.HasRequired(p => p.Receiver).WithMany().WillCascadeOnDelete(false);
        }

        #endregion
    }
}