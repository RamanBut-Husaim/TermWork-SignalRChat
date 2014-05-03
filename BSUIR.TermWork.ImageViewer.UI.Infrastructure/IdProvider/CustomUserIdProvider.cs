using System;
using System.Globalization;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication;

using Microsoft.AspNet.SignalR;

namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure.IdProvider
{
    public sealed class CustomUserIdProvider : IUserIdProvider
    {
        #region Implementation of IUserIdProvider

        public string GetUserId(IRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var userIdentity = request.User.Identity as CustomIdentity;
            if (userIdentity != null)
            {
                return userIdentity.Id.ToString(CultureInfo.InvariantCulture);
            }

            return string.Empty;
        }
        #endregion
    }
}
