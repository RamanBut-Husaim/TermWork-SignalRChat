// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommentMap.cs" company="">
//   
// </copyright>
// <summary>
//   The comment map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    /// <summary>
    /// The comment map.
    /// </summary>
    internal sealed class CommentMap : EntityTypeConfiguration<Comment>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentMap"/> class.
        /// </summary>
        public CommentMap()
        {
            this.HasKey(p => p.Key);
            Property(p => p.Key).IsRequired();
            Property(p => p.Content).IsRequired().HasMaxLength(Comment.MaxLengthFor.Content);
            Property(p => p.CreationDate).IsRequired();
            Property(p => p.Rate).IsRequired();

            this.HasRequired(p => p.Image).WithMany().WillCascadeOnDelete(true);
            this.HasRequired(p => p.Owner).WithMany().WillCascadeOnDelete(false);
        }

        #endregion
    }
}