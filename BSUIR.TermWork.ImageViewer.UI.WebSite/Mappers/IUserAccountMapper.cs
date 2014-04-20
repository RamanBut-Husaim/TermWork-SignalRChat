using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    using BSUIR.TermWork.ImageViewer.Model;
    using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Account;

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
