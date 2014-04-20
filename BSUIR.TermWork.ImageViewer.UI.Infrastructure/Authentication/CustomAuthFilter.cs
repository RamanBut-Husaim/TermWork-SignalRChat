namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    using BSUIR.TermWork.ImageViewer.Model;

    public sealed class CustomAuthFilter : AuthorizeAttribute
	{
		private readonly List<string> _roles;

		public CustomAuthFilter(string roleString = null)
		{
			this._roles = new List<string>();
			if (string.IsNullOrEmpty(roleString))
			{
				this._roles.AddRange(new string[]
					{
						RoleName.Administrator.ToString(),
						RoleName.Moderator.ToString(),
                        RoleName.RegisteredUser.ToString()
					});
			}
			else
			{
				string[] temp = roleString.Split(',');
				temp = temp.Select(t => t.Trim(' ')).ToArray();
				this._roles.AddRange(temp);
			}
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			bool result = false;
			var authCookies = 
				httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
			if (authCookies != null)
			{
				if (this._roles.Any(role => httpContext.User.IsInRole(role)))
				{
					result = true;
				}
			}
			return result;
		}
	}
}
