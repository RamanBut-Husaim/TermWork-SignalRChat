﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserMap.cs" company="">
//   
// </copyright>
// <summary>
//   The user map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    /// <summary>
    /// The user map.
    /// </summary>
    internal sealed class UserMap : EntityTypeConfiguration<User>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMap"/> class.
        /// </summary>
        public UserMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.Email).IsRequired().HasMaxLength(User.MaxLengthFor.Email);
            this.Property(p => p.PasswordHash)
                .IsRequired()
                .HasMaxLength(User.MaxLengthFor.PasswordHash);
            this.Property(p => p.PasswordSalt)
                .IsRequired()
                .HasMaxLength(User.MaxLengthFor.PasswordSalt);
            this.HasMany(r => r.UserRoles).WithMany().Map(
                m =>
                    {
                        m.MapLeftKey("UserKey");
                        m.MapRightKey("RoleKey");
                        m.ToTable("UserRole");
                    });
            this.HasRequired(p => p.UserProfile).WithRequiredPrincipal().WillCascadeOnDelete(false);
        }

        #endregion
    }
}