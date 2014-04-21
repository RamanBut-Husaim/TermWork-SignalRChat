// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProfileMap.cs" company="">
//   
// </copyright>
// <summary>
//   The profile map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    /// <summary>
    /// The profile map.
    /// </summary>
    internal sealed class ProfileMap : EntityTypeConfiguration<Profile>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileMap"/> class.
        /// </summary>
        public ProfileMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(Profile.MaxLengthFor.FirstName);
            this.Property(p => p.LastName).IsRequired().HasMaxLength(Profile.MaxLengthFor.LastName);
            this.Property(p => p.RegistrationDate).IsRequired();
            this.Property(p => p.IsSignedIn).IsRequired();
            this.Property(p => p.LastSignIn).IsRequired();
            this.Property(p => p.LastSignOut).IsRequired();
            this.HasRequired(p => p.User).WithRequiredDependent().WillCascadeOnDelete(false);
            this.HasMany(p => p.Subscriptions)
                .WithRequired(p => p.Subscriber)
                .WillCascadeOnDelete(false);
            this.HasMany(p => p.FriendshipRequests)
                .WithRequired(p => p.Sender)
                .WillCascadeOnDelete(false);
        }

        #endregion
    }
}