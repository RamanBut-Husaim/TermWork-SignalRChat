namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication
{
    using System;
    using System.Web.Security;

    public static class CustomAuthenticationExtensions
	{
		public static FormsAuthenticationTicket CreateTicket(this UserInfo userInfo, Boolean isPersistent)
		{
			return new FormsAuthenticationTicket(1, userInfo.LastName, DateTime.Now,
			                                     DateTime.Now.AddHours(10), isPersistent, 
												 userInfo.ToString());
		}
		
		public static UserInfo CreateUserInfo(this FormsAuthenticationTicket ticket)
		{
			return UserInfo.FromString(ticket.UserData);
		}
	}
}
