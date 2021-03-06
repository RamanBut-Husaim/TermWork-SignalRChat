﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitializeImageViewerAlways.cs" company="">
//   
// </copyright>
// <summary>
//   The initialize image viewer always.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Data.Entity;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Shared;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Initialization
{
    /// <summary>
    ///     The initialize image viewer always.
    /// </summary>
    internal sealed class InitializeImageViewerAlways : DropCreateDatabaseAlways<ImageViewerContext>
    {
        #region Methods

        /// <summary>
        /// The seed.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected override void Seed(ImageViewerContext context)
        {
            var accessRight01 = new AccessRight(
                AccessRightName.UploadImage, 
                AccessRightName.UploadImage.ToString());
            var accessRight02 = new AccessRight(
                AccessRightName.RemoveImage, 
                AccessRightName.RemoveImage.ToString());
            var accessRight03 = new AccessRight(
                AccessRightName.MakeComment, 
                AccessRightName.MakeComment.ToString());
            var accessRight04 = new AccessRight(
                AccessRightName.RemoveComment, 
                AccessRightName.RemoveComment.ToString());
            var accessRight05 = new AccessRight(
                AccessRightName.Subscribe, 
                AccessRightName.Subscribe.ToString());
            var accessRight06 = new AccessRight(
                AccessRightName.RemoveUser, 
                AccessRightName.RemoveUser.ToString());

            context.Set<AccessRight>().Add(accessRight01);
            context.Set<AccessRight>().Add(accessRight02);
            context.Set<AccessRight>().Add(accessRight03);
            context.Set<AccessRight>().Add(accessRight04);
            context.Set<AccessRight>().Add(accessRight05);
            context.Set<AccessRight>().Add(accessRight06);
            context.SaveChanges();

            var role01 = new Role(RoleName.Administrator, RoleName.Administrator.ToString())
                {
                    AccessRights =
                        new Collection<AccessRight>
                            {
                                accessRight01, 
                                accessRight02, 
                                accessRight03, 
                                accessRight04, 
                                accessRight05, 
                                accessRight06
                            }
                };
            var role02 = new Role(RoleName.RegisteredUser, RoleName.RegisteredUser.ToString())
                {
                    AccessRights =
                        new Collection<AccessRight>
                            {
                                accessRight01, 
                                accessRight02, 
                                accessRight03, 
                                accessRight05
                            }
                };
            var role03 = new Role(RoleName.UnregisteredUser, RoleName.UnregisteredUser.ToString())
                {
                    AccessRights = new Collection<AccessRight>()
                };
            var role04 = new Role(RoleName.Moderator, RoleName.Moderator.ToString())
                {
                    AccessRights =
                        new Collection<AccessRight>
                            {
                                accessRight01, 
                                accessRight02, 
                                accessRight03, 
                                accessRight04, 
                                accessRight05
                            }
                };

            var user = new User("halford@gmail.com");
            user.PasswordSalt = CryptoHelper.GenerateSalt(User.MaxLengthFor.PasswordSalt);
            user.PasswordHash = CryptoHelper.ComputePasswordHash("passwordd", user.PasswordSalt);
            user.UserRoles.Add(role01);
            user.UserRoles.Add(role02);
            user.UserRoles.Add(role04);
            var profile = new Profile(user);
            user.UserProfile = profile;
            profile.FirstName = "Rob";
            profile.LastName = "Halford";
            profile.LastSignIn = DateTime.Now;
            profile.LastSignOut = DateTime.Now;
            profile.RegistrationDate = DateTime.Now;
            profile.IsSignedIn = false;

            var album01 = new Album();
            album01.CreationDate = DateTime.UtcNow;
            album01.Owner = user;
            album01.Name = "The first one";
            album01.Description = album01.Name;
            album01.ImageNumber = 0;

            var album02 = new Album();
            album02.CreationDate = DateTime.UtcNow;
            album02.Owner = user;
            album02.Name = "The second one";
            album02.Description = album02.Name;
            album02.ImageNumber = 0;

            var subscriptionType01 = new SubscriptionType
                {
                    Name = SubscriptionName.Album, 
                    Description = SubscriptionName.Album.ToString()
                };
            var subscriptionType02 = new SubscriptionType
                {
                    Name = SubscriptionName.Image, 
                    Description = SubscriptionName.Image.ToString()
                };

            context.Set<Role>().Add(role01);
            context.Set<Role>().Add(role02);
            context.Set<Role>().Add(role03);
            context.Set<Role>().Add(role04);
            context.Set<User>().Add(user);
            context.Set<Profile>().Add(profile);
            context.Set<Album>().Add(album01);
            context.Set<Album>().Add(album02);
            context.Set<SubscriptionType>().Add(subscriptionType01);
            context.Set<SubscriptionType>().Add(subscriptionType02);

            var user01 = new User("dickinson@gmail.com");
            user01.PasswordSalt = CryptoHelper.GenerateSalt(User.MaxLengthFor.PasswordSalt);
            user01.PasswordHash = CryptoHelper.ComputePasswordHash("passwordd", user01.PasswordSalt);
            user01.UserRoles.Add(role01);
            user01.UserRoles.Add(role02);
            user01.UserRoles.Add(role04);
            var profile01 = new Profile(user01);
            user01.UserProfile = profile01;
            profile01.FirstName = "Bruce";
            profile01.LastName = "Dickinson";
            profile01.LastSignIn = DateTime.Now;
            profile01.LastSignOut = DateTime.Now;
            profile01.RegistrationDate = DateTime.Now;
            profile01.IsSignedIn = false;

            var album03 = new Album();
            album03.CreationDate = DateTime.UtcNow;
            album03.Owner = user01;
            album03.Name = "The first one";
            album03.Description = album03.Name;
            album03.ImageNumber = 0;

            var album04 = new Album();
            album04.CreationDate = DateTime.UtcNow;
            album04.Owner = user01;
            album04.Name = "The second one";
            album04.Description = album04.Name;
            album04.ImageNumber = 0;

            context.Set<User>().Add(user01);
            context.Set<Profile>().Add(profile01);
            context.Set<Album>().Add(album03);
            context.Set<Album>().Add(album04);

            base.Seed(context);
        }

        #endregion
    }
}