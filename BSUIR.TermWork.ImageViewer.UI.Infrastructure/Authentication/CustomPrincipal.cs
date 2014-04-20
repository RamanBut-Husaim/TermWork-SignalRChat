namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication
{
    using System;
    using System.Security.Principal;

    public sealed class CustomPrincipal : IPrincipal
    {
        private readonly IIdentity _identity;
        private readonly string[] _roles;

        public bool IsInRole(string role)
        {
            bool result = false;
            if (string.IsNullOrEmpty(role))
            {
                return result;
            }
            for (int i = 0; i < this._roles.Length; ++i)
            {
                if (this._roles[i] != null
                    && string.Compare(this._roles[i], role, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public IIdentity Identity
        {
            get { return this._identity; }
        }

        public CustomPrincipal(IIdentity identity, string[] roles)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            this._identity = identity;

            if (roles != null)
            {
                this._roles = new string[roles.Length];
                for (int index = 0; index < roles.Length; ++index)
                {
                    this._roles[index] = roles[index];
                }
            }
            else
            {
                this._roles = new string[0];
            }
        }
    }
}
