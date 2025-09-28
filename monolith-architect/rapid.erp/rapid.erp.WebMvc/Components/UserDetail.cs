using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rapid.erp.Core.Helpers;
using rapid.erp.Core.Security;
using rapid.erp.ViewModel.Security;

namespace rapid.erp.WebMvc.Components
{
    public class UserDetail: ViewComponent
    {
        #region Global Variable Declaration
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserDetail> _iLogger;
        #endregion

        #region Constructor
        public UserDetail(ILogger<UserDetail> iLogger, UserManager<ApplicationUser> userManager)
        {
            _iLogger = iLogger;
            _userManager = userManager;
        }
        #endregion

        #region Actions
        public async Task<IViewComponentResult> InvokeAsync()
        {
            LoggedUserViewModel loggedUserViewModel = new LoggedUserViewModel();
            try
            {
                //string userName = HttpContext.User.Identity.Name;
                loggedUserViewModel = await GetUser();
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "InvokeAsync"));
            }
            return View(loggedUserViewModel);
        }

        public async Task<LoggedUserViewModel> GetUser()
        {
            LoggedUserViewModel loggedUserViewModel = new LoggedUserViewModel();
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
                var roles = await _userManager.GetRolesAsync(user);
                
                loggedUserViewModel.UserName = user?.UserName;
                loggedUserViewModel.Role = roles.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LogMessageHelper.FormateMessageForException(ex, "GetUser"));
            }
            return loggedUserViewModel;
        }
        #endregion
    }
}
