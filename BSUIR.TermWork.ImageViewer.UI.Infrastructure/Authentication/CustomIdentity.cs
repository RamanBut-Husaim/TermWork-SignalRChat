namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication
{
    using System;
    using System.Security.Principal;

    public sealed class CustomIdentity : ICustomIdentity
	{
		public string Name { get; private set; }
		public string AuthenticationType { get; private set; }
		public bool IsAuthenticated { get; private set; }

		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public int Id { get; private set; }

        public string[] Roles
        {
            get;
            private set;
        }

		public CustomIdentity(UserInfo userInfo)
		{
			this.Name = "CustomIdentity";
			this.AuthenticationType = "Custom";
			this.IsAuthenticated = true;
			this.FirstName = userInfo.FirstName;
			this.LastName = userInfo.LastName;
			this.Id = userInfo.Id;
		    if (userInfo.Roles != null)
		    {
		        this.Roles = new string[userInfo.Roles.Length];
		        for (int i = 0; i < userInfo.Roles.Length; ++i)
		        {
		            this.Roles[i] = userInfo.Roles[i];
		        }
		    }
		    else
		    {
		        this.Roles = new string[0];
		    }
		}

	}
}
