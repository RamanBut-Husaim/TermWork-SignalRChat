namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication
{
    using System;
    using System.Linq;
    using System.Web.Helpers;
    using System.Web.Security;
    using System.Xml.Linq;

    using BSUIR.TermWork.ImageViewer.Model;

    public sealed class UserInfo
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

        public string[] Roles
        {
            get;
            set;
        }

		public UserInfo(User user)
		{
			this.Id = user.Key;
			this.FirstName = user.UserProfile.FirstName;
			this.LastName = user.UserProfile.LastName;
		    this.Roles = user.UserRoles.Select(p => p.Name.ToString()).ToArray();
		}

		private UserInfo() {}

		public override string ToString()
		{
			var element = new XElement("User",
			                           new XElement("Id", this.Id),
			                           new XElement("FirstName", this.FirstName),
			                           new XElement("LastName", this.LastName),
                                       new XElement("Roles", this.Roles.Select(p => new XElement("Role", p))));
			return element.ToString();
		}

		public static UserInfo FromString(string data)
		{
			var resultUser = new UserInfo();
			try
			{
				XElement element = XElement.Parse(data);
				resultUser.Id = int.Parse(element.Element("Id").Value);
				resultUser.FirstName = element.Element("FirstName").Value;
				resultUser.LastName = element.Element("LastName").Value;
			    resultUser.Roles = element.Elements("Roles")
                                          .Elements("Role")
                                          .Select(p => p.Value)
                                          .ToArray();
			}
			catch (Exception ex)
			{
                new CustomAuthentication().SignOut();
				throw new ArgumentException("Invalid user name object!", ex);
			}
			return resultUser;
		}
	}
}
