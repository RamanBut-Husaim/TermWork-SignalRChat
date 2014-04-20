namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication
{
    using System;
    using System.Web;
    using System.Web.Security;

    public sealed class CustomAuthenticationHttpModule : IHttpModule
    {
        #region Fields

        private HttpApplication _context;

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            this._context = context;
            this._context.AuthenticateRequest += this.OnAuthentication;
        }

        #endregion

        #region Methods

        private void OnAuthentication(object sender, EventArgs args)
        {
            var context = sender as HttpApplication;

            HttpCookie authCookie = context.Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = new CustomAuthentication().Decrypt(authCookie.Value);
                UserInfo userInfo = ticket.CreateUserInfo();
                context.Context.User = new CustomPrincipal(new CustomIdentity(userInfo), userInfo.Roles);
            }
        }

        #endregion
    }
}