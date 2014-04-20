namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication
{
    using System.Security.Principal;

    public interface ICustomIdentity : IIdentity
    {
        #region Public Properties

        string FirstName
        {
            get;
        }

        int Id
        {
            get;
        }

        string LastName
        {
            get;
        }

        string[] Roles
        {
            get;
        }

        #endregion
    }
}