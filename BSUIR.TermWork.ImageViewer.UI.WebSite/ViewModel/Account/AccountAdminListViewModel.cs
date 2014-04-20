using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Account
{
    using BSUIR.TermWork.ImageViewer.Model;

    public class AccountAdminListViewModel
    {
        public int Key
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public DateTime RegistrationDate
        {
            get;
            set;
        }

        public string[] Roles
        {
            get;
            set;
        }

        public bool IsModerator
        {
            get
            {
                if (this.Roles != null)
                {
                    return this.Roles.Contains(RoleName.Moderator.ToString());
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsAdmin
        {
            get
            {
                if (this.Roles != null)
                {
                    return this.Roles.Contains(RoleName.Administrator.ToString());
                }
                else
                {
                    return false;
                }
            }
        }
    }
}