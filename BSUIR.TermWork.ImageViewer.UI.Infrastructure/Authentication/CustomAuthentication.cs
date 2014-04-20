namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication
{
    using System;
    using System.Web;
    using System.Web.Security;

    public sealed class CustomAuthentication : ICustomAuthentication
	{
		public void SignOut()
		{
			FormsAuthentication.SignOut();
		}

		public FormsAuthenticationTicket Decrypt(String encryptedTicket)
		{
			if (encryptedTicket == null)
			{
				throw new ArgumentNullException("encryptedTicket");
			}
			return FormsAuthentication.Decrypt(encryptedTicket);
		}

		public void SetAuthCookie(HttpContextBase context, FormsAuthenticationTicket ticket)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (ticket == null)
			{
				throw new ArgumentNullException("ticket");
			}
			String encryptedTicket = FormsAuthentication.Encrypt(ticket);
			context.Response.Cookies.Add(
				new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));
		}
	}
}
