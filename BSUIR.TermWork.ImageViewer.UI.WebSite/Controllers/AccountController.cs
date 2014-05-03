using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Filters;
using BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Account;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : BaseController
    {
        private readonly IMembershipService _membershipService;
        private readonly IUserAccountMapper _userAccountMapper;

        public AccountController(IMembershipService membershipService, IUserAccountMapper userAccountMapper)
        {
            this._membershipService = membershipService;
            this._userAccountMapper = userAccountMapper;
        }

        [HttpGet]
        [Route("userinfo")]
        public ActionResult GetUserInfo()
        {
            var accountUserInfoViewModel = new AccountInfoViewModel();
            var customIdentity = this.User.Identity as CustomIdentity;
            if (customIdentity != null)
            {
                accountUserInfoViewModel.Key = customIdentity.Id;
                accountUserInfoViewModel.FirstName = customIdentity.FirstName;
                accountUserInfoViewModel.LastName = customIdentity.LastName;
            }
            else
            {
                accountUserInfoViewModel.FirstName = Resources.Resources.Account_Unknown;
                accountUserInfoViewModel.LastName = Resources.Resources.Account_User;
            }

            return this.PartialView("_GetUserInfo", accountUserInfoViewModel);
        }

        [HttpGet]
        [Route("edit/{key:int}")]
        [AjaxRequestOnlyFilter]
        public PartialViewResult EditUser(int? key)
        {
            AccountInfoViewModel result;

            if (key.HasValue)
            {
                try
                {
                    User user = this._membershipService.GetUserByKey(key.Value);
                    if (user != null)
                    {
                        result = this._userAccountMapper.BuildInfo(user);
                    }
                    else
                    {
                        this.TempData[Constants.TempDataErrorMessage] = "User is missing.";
                        return this.PartialView("_ErrorModal");
                    }
                }
                catch (Exception ex)
                {
                    this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                    return this.PartialView("_ErrorModal");
                }
            }
            else
            {
                this.TempData[Constants.TempDataErrorMessage] = "User is missing.";
                return this.PartialView("_ErrorModal");
            }

            return this.PartialView("_EditUser", result);
        }

        [HttpPost]
        [Route("edit/{key:int}")]
        [AjaxRequestOnlyFilter]
        public ActionResult Edit(AccountInfoViewModel viewModel)
        {
            bool result = false;
            var user = this.User.Identity as CustomIdentity;

            if (this.ModelState.IsValid && viewModel != null)
            {
                try
                {
                    User sourceUser = this._membershipService.GetUserByKey(user.Id);
                    if (sourceUser != null)
                    {
                        this._userAccountMapper.UpdateUser(sourceUser, viewModel);
                        this._membershipService.UpdateUser(sourceUser);
                    }

                    result = true;
                }
                catch (Exception)
                {
                    this.ModelState.AddModelError(string.Empty, Resources.Resources.OperationFailure);
                }
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, Resources.Resources.OperationFailure);
            }

            if (result)
            {
                return this.Json(new { success = result });
            }

            return this.PartialView("_EditUser", viewModel);
        }

        [HttpGet]
        [Route("signin")]
        [AjaxRequestOnlyFilter]
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            AccountSignInViewModel signInViewModel = null;
            if (!this.Request.IsAuthenticated)
            {
                signInViewModel = new AccountSignInViewModel();
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
            return this.PartialView("_SignIn", signInViewModel);
        }

        [HttpPost]
        [Route("signin")]
        [AjaxRequestOnlyFilter]
        [AllowAnonymous]
        public ActionResult SignIn(AccountSignInViewModel accountSignInViewModel)
        {
            this.ViewBag.ReturnUrl = string.Empty;
            bool result = false;

            if (accountSignInViewModel != null && this.ModelState.IsValid)
            {
                try
                {
                    User user = this._membershipService.SignIn(
                        accountSignInViewModel.Email,
                        accountSignInViewModel.Password);
                    this.SetAuthCookie(user);
                    result = true;
                }
                catch (AuthenticationException ex)
                {
                    this.ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, Resources.Resources.OperationFailure);
            }

            if (result)
            {
                return this.Json(new { success = true });
            }

            return this.PartialView("_SignIn", accountSignInViewModel);
        }

        [HttpGet]
        [Route("signout")]
        [CustomAuthFilter]
        public ActionResult SignOut()
        {
            new CustomAuthentication().SignOut();
            var identity = this.HttpContext.User.Identity as CustomIdentity;
            if (identity != null)
            {
                try
                {
                    this._membershipService.SignOut(identity.Id);
                }
                catch (Exception ex)
                {
                    this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                    return this.RedirectToAction("Error", "Home");
                }
            }

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("register")]
        [AjaxRequestOnlyFilter]
        [AllowAnonymous]
        public ActionResult Register()
        {
            AccountRegisterViewModel accountRegisterViewModel;
            if (!this.Request.IsAuthenticated)
            {
                accountRegisterViewModel = new AccountRegisterViewModel();
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.PartialView("_Register", accountRegisterViewModel);
        }

        [HttpPost]
        [Route("register")]
        [AjaxRequestOnlyFilter]
        [AllowAnonymous]
        public ActionResult Register(AccountRegisterViewModel accountRegisterViewModel)
        {
            bool result = false;
            if (accountRegisterViewModel != null && this.ModelState.IsValid)
            {
                try
                {
                    User user = this._userAccountMapper.BuildRegister(accountRegisterViewModel);
                    this._membershipService.CreateUser(user);
                    this.SetAuthCookie(user);
                    result = true;
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, Resources.Resources.OperationFailure);
            }

            if (result)
            {
                return this.Json(new { success = result });
            }

            return this.PartialView("_Register", accountRegisterViewModel);
        }

        [HttpGet]
        [Route("~/admin/user/addmoderatorrole/{userKey:int}")]
        [CustomAuthFilter("Administrator")]
        public ActionResult AddModeratorRole(int? userKey)
        {
            try
            {
                if (userKey.HasValue)
                {
                    User user = this._membershipService.GetUserByKey(userKey.Value);
                    this._membershipService.AddRoleToUser(user, RoleName.Moderator);
                }
            }
            catch (Exception ex)
            {
                this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("Index", "Account");
        }

        [HttpGet]
        [Route("~/admin/user/addadminrole/{userKey:int}")]
        [CustomAuthFilter("Administrator")]
        public ActionResult AddAdminRole(int? userKey)
        {
            try
            {
                if (userKey.HasValue)
                {
                    User user = this._membershipService.GetUserByKey(userKey.Value);
                    this._membershipService.AddRoleToUser(user, RoleName.Administrator);
                }
            }
            catch (Exception ex)
            {
                this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("Index", "Account");
        }

        [HttpGet]
        [Route("~/admin/user/removeadminrole/{userKey:int}")]
        [CustomAuthFilter("Administrator")]
        public ActionResult RemoveAdminRole(int? userKey)
        {
            try
            {
                if (userKey.HasValue)
                {
                    User user = this._membershipService.GetUserByKey(userKey.Value);
                    this._membershipService.RemoveRoleFromUser(user, RoleName.Administrator);
                }
            }
            catch (Exception ex)
            {
                this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("Index", "Account");
        }

        [HttpGet]
        [Route("~/admin/user/removemoderatorrole/{userKey:int}")]
        [CustomAuthFilter("Administrator")]
        public ActionResult RemoveModeratorRole(int? userKey)
        {
            try
            {
                if (userKey.HasValue)
                {
                    User user = this._membershipService.GetUserByKey(userKey.Value);
                    this._membershipService.RemoveRoleFromUser(user, RoleName.Moderator);
                }
            }
            catch (Exception ex)
            {
                this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("Index", "Account");
        }

        [HttpGet]
        [Route("~/admin/users/edituser/{userKey:int}")]
        [AjaxRequestOnlyFilter]
        public PartialViewResult AdminEditUser(int? userKey)
        {
            AccountEditViewModel result;

            if (userKey.HasValue)
            {
                try
                {
                    User user = this._membershipService.GetUserByKey(userKey.Value);
                    if (user != null)
                    {
                        result = this._userAccountMapper.BuildEdit(user);
                    }
                    else
                    {
                        this.TempData[Constants.TempDataErrorMessage] = "User is missing.";
                        return this.PartialView("_ErrorModal");
                    }
                }
                catch (Exception ex)
                {
                    this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                    return this.PartialView("_ErrorModal");
                }
            }
            else
            {
                this.TempData[Constants.TempDataErrorMessage] = "User is missing.";
                return this.PartialView("_ErrorModal");
            }

            return this.PartialView("_AdminEditUser", result);
        }

        [HttpPost]
        [Route("~/admin/users/edituser/{userKey:int}")]
        [AjaxRequestOnlyFilter]
        [CustomAuthFilter("Administrator")]
        public ActionResult AdminEditUser(int? userKey, AccountEditViewModel viewModel)
        {
            bool result = false;

            if (this.ModelState.IsValid && viewModel != null && userKey.HasValue)
            {
                try
                {
                    User sourceUser = this._membershipService.GetUserByKey(userKey.Value);
                    if (sourceUser != null)
                    {
                        this._userAccountMapper.UpdateUser(sourceUser, viewModel);
                        this._membershipService.UpdateUser(sourceUser);
                    }

                    result = true;
                }
                catch (Exception)
                {
                    this.ModelState.AddModelError(string.Empty, Resources.Resources.OperationFailure);
                }
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, Resources.Resources.OperationFailure);
            }

            if (result)
            {
                return this.Json(new { success = result });
            }

            return this.PartialView("_AdminEditUser", viewModel);
        }

        [HttpGet]
        [Route("~/admin/users/removeuser/{userKey:int}")]
        [CustomAuthFilter("Administrator")]
        public ActionResult RemoveUser(int? userKey)
        {
            try
            {
                if (userKey.HasValue)
                {
                    this._membershipService.RemoveUser(userKey.Value);
                }
            }
            catch (Exception ex)
            {
                this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("Index", "Account");
        }

        [HttpGet]
        [Route("~/admin/navbaruserlist")]
        [CustomAuthFilter("Administrator")]
        [ChildActionOnly]
        public PartialViewResult NavbarUserList()
        {
            IList<AccountSimpleViewModel> result = new List<AccountSimpleViewModel>();
            try
            {
                IList<User> users = this._membershipService.GetAllUsers();
                result = users.Select(p => this._userAccountMapper.BuildSimple(p)).ToList();
            }
            catch (Exception ex)
            {
                this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                return this.PartialView("_ErrorModal");
            }

            return this.PartialView("_NavbarUserList", result);
        }

        [HttpGet]
        [Route("~/admin/userlist")]
        [CustomAuthFilter("Administrator")]
        public ActionResult UserList()
        {
            IList<AccountAdminListViewModel> result = new List<AccountAdminListViewModel>();
            try
            {
                IList<User> users = this._membershipService.GetAllUsers();
                result = users.Select(p => this._userAccountMapper.BuildAdminList(p)).ToList();
            }
            catch (Exception ex)
            {
                this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                return this.RedirectToAction("Error", "Home");
            }

            return this.PartialView("_UserList", result);
        }

        [HttpGet]
        [Route("~/admin/index")]
        [CustomAuthFilter("Administrator")]
        public ViewResult Index()
        {
            return this.View("Index");
        }

        [HttpGet]
        [Route("~/admin/navigationbar")]
        [ChildActionOnly]
        [CustomAuthFilter("Administrator")]
        public PartialViewResult NavigationBar()
        {
            return this.PartialView("_NavigationBar");
        }

        private void SetAuthCookie(User user)
        {
            var userAuthInfo = new UserInfo(user);
            FormsAuthenticationTicket ticket = userAuthInfo.CreateTicket(false);
            new CustomAuthentication().SetAuthCookie(this.HttpContext, ticket);
        }
    }
}