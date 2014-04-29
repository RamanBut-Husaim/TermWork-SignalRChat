using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Account;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    public interface IUserAccountMapper
    {
        User BuildRegister(AccountRegisterViewModel viewModel);

        AccountRegisterViewModel BuildUser(User user);

        AccountInfoViewModel BuildInfo(User user);

        void UpdateUser(User user, AccountInfoViewModel viewModel);

        void UpdateUser(User user, AccountEditViewModel viewModel);

        AccountAdminListViewModel BuildAdminList(User user);

        AccountSimpleViewModel BuildSimple(User user);

        AccountEditViewModel BuildEdit(User user);
    }
}
