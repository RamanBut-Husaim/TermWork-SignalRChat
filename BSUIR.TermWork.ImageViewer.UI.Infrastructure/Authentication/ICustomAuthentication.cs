namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication
{
    using System.Web;
    using System.Web.Security;

    public interface ICustomAuthentication
	{
		void SignOut();
		FormsAuthenticationTicket Decrypt(string encryptedTicket);
		void SetAuthCookie(HttpContextBase context, FormsAuthenticationTicket ticket);
	}
}
