// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FriendshipRequestMap.cs" company="">
//   
// </copyright>
// <summary>
//   The friendship request map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    /// <summary>
    /// The friendship request map.
    /// </summary>
    internal sealed class FriendshipRequestMap : EntityTypeConfiguration<FriendshipRequest>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipRequestMap"/> class.
        /// </summary>
        public FriendshipRequestMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.IsConfirmed).IsRequired();
            this.Property(p => p.CreationdDate).IsRequired();
            this.HasRequired(p => p.Sender).WithMany().WillCascadeOnDelete(false);
            this.HasRequired(p => p.Receiver).WithMany().WillCascadeOnDelete(false);
        }

        #endregion
    }
}