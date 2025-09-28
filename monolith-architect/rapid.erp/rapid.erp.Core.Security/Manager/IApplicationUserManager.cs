using Microsoft.AspNetCore.Mvc.Rendering;
using rapid.erp.Common;
using rapid.erp.Core.Utility;
using rapid.erp.ViewModel.Security;

namespace rapid.erp.Core.Security.Manager
{
    public interface IApplicationUserManager
    {
        Task<List<ApplicationUserViewModel>> GetApplicationUsersAsync();

        Task<ApplicationUserViewModel> GetApplicationUserAsync(string key);

        Task<List<ApplicationUserViewModel>> GetApplicationUsersAsync(bool isActive, CancellationToken cancellationToken = default);

        Task<ApplicationUserEditViewModel> GetApplicationUserForEditAsync(string key);

        Task<AppResult> UpdateApplicationUserAsync(ApplicationUserEditViewModel model);

        Task<SearchResult<IEnumerable<ApplicationUserViewModel>>> GetApplicationUsersSearchResultAsync(SearchModel searchModel, bool pagination);

        Task<DefaultPageViewModel<ApplicationUserCreateViewModel, SearchModel, SearchResult<IEnumerable<ApplicationUserViewModel>>>> GetApplicationUsersDefaultPageViewModelAsync(SearchModel searchModel);

        Task<DefaultPageViewModel<ApplicationUserCreateViewModel, SearchModel, SearchResult<IEnumerable<ApplicationUserViewModel>>>> GetApplicationUsersDefaultPageViewModelAsync(SearchModel searchModel, bool pagination);

        Task<AppResult> CreateApplicationUserAsync(ApplicationUserCreateViewModel model, CancellationToken cancellationToken = default);

        Task<AppResult> DeleteApplicationUserAsync(string key, CancellationToken cancellationToken = default);

        Task<List<SelectListItem>> GetApplicationUserDropDownViewModelAsync(bool isActive);

        Task<ApplicationUserViewModel> GetApplicationUserByUserNameOrEmailAsync(string userNameOrEmail);

        Task<AppResult> IsInRoleAsync(ApplicationUserViewModel model, string roleName);

        Task<AppResult> CreateApplicationUserWithRoleAsync(RegisterViewModel model, CancellationToken cancellationToken = default);

        Task<AppResult> IsEmailConfirmedAsync(ApplicationUserViewModel model);

        Task<string> GeneratePasswordResetTokenAsync(ApplicationUserViewModel model);

        Task<AppResult> ResetPasswordAsync(ApplicationUserViewModel model, string token, string password);
    }
}
