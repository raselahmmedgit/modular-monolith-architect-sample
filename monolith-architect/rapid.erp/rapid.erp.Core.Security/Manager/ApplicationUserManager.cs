using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rapid.erp.Common;
using rapid.erp.Core.Extensions;
using rapid.erp.Core.Helpers;
using rapid.erp.Core.Security;
using rapid.erp.Core.Utility;
using rapid.erp.ViewModel.Security;

namespace rapid.erp.Core.Security.Manager
{
    public class ApplicationUserManager : IApplicationUserManager
    {
        private UserManager<ApplicationRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        private IMapper _mapper;

        public ApplicationUserManager(IMapper mapper, UserManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<List<ApplicationUserViewModel>> GetApplicationUsersAsync()
        {
            var data = await _userManager.Users.ToListAsync();
            var pp = _mapper.Map<List<ApplicationUser>, List<ApplicationUserViewModel>>(data);
            return pp;
        }
        
        public async Task<ApplicationUserViewModel> GetApplicationUserAsync(string key)
        {
            var data = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == key);
            var pp = _mapper.Map<ApplicationUser, ApplicationUserViewModel>(data);
            return pp;
        }

        public async Task<ApplicationUserEditViewModel> GetApplicationUserForEditAsync(string key)
        {
            var data = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == key);
            var pp = _mapper.Map<ApplicationUser, ApplicationUserEditViewModel>(data);
            return pp;
        }

        public async Task<List<ApplicationUserViewModel>> GetApplicationUsersAsync(bool isActive, CancellationToken cancellationToken = default)
        {
            var data = await _userManager.Users.Where(x => x.IsActive == isActive).ToListAsync(cancellationToken);
            var pp = _mapper.Map<List<ApplicationUser>, List<ApplicationUserViewModel>>(data);
            return pp;
        }

        public async Task<SearchResult<IEnumerable<ApplicationUserViewModel>>> GetApplicationUsersSearchResultAsync(SearchModel searchModel, bool pagination)
        {
            string? st = searchModel.SearchText?.Trim().ToLower();
            var query = _userManager.Users.Where(x => x.IsDeleted != true).AsQueryable();
            if (!string.IsNullOrEmpty(searchModel.SearchText))
            {
                query = query.Where(x => x.UserName.ToLower().Contains(st)
                    || x.FirstName.ToLower().StartsWith(st)
                    || x.LastName.ToLower().StartsWith(st)
                    || x.Email.ToLower().StartsWith(st)
                    || x.NormalizedUserName.ToLower().StartsWith(st)
                    || x.NormalizedEmail.ToLower().StartsWith(st));
            }
            var totalCount = await query.CountAsync();
            var dataQuery = query.OrderByName(searchModel.SortColumn, searchModel.IsDescending);
            if (pagination == true)
            {
                dataQuery = dataQuery.Skip((searchModel.GetCurrentPage() - 1) * (searchModel.Rows)).Take(searchModel.Rows);
            }
            
            var dataList = await dataQuery.ToListAsync();
            var dataCount = dataList.Count;
            var data = new SearchResult<IEnumerable<ApplicationUser>>(dataList, totalCount, dataCount, true, "");

            var dataMapped = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(data.Value);
            var result = new SearchResult<IEnumerable<ApplicationUserViewModel>>(dataMapped, data.TotalRows, data.PageRows, true, "");
            return result;
        }
        
        public async Task<DefaultPageViewModel<ApplicationUserCreateViewModel, SearchModel, SearchResult<IEnumerable<ApplicationUserViewModel>>>> GetApplicationUsersDefaultPageViewModelAsync(SearchModel searchModel)
        {
            var resultData = await GetApplicationUsersSearchResultAsync(searchModel, true);
            var returnModel = new DefaultPageViewModel<ApplicationUserCreateViewModel, SearchModel, SearchResult<IEnumerable<ApplicationUserViewModel>>>()
            {
                CreateorEditViewModel = new ApplicationUserCreateViewModel(),
                SearchOptionViewModel = searchModel,
                SearchResultViewModel = resultData
            };
            return returnModel;
        }
        
        public async Task<DefaultPageViewModel<ApplicationUserCreateViewModel, SearchModel, SearchResult<IEnumerable<ApplicationUserViewModel>>>> GetApplicationUsersDefaultPageViewModelAsync(SearchModel searchModel, bool pagination)
        {
            var resultData = await GetApplicationUsersSearchResultAsync(searchModel, pagination);
            var returnModel = new DefaultPageViewModel<ApplicationUserCreateViewModel, SearchModel, SearchResult<IEnumerable<ApplicationUserViewModel>>>()
            {
                CreateorEditViewModel = new ApplicationUserCreateViewModel(),
                SearchOptionViewModel = searchModel,
                SearchResultViewModel = resultData
            };
            return returnModel;
        }
        
        public async Task<AppResult> CreateApplicationUserAsync(ApplicationUserCreateViewModel model, CancellationToken cancellationToken = default)
        {
            var dataMapped = _mapper.Map<ApplicationUserCreateViewModel, ApplicationUser>(model);
            var identityResult = await _userManager.CreateAsync(dataMapped);
            if (identityResult.Succeeded == true)
            {
                return AppResult.Ok(MessageHelper.Save);
            }
            else
            {
                return AppResult.Fail(MessageHelper.SaveFail);
            }
        }

        public async Task<AppResult> UpdateApplicationUserAsync(ApplicationUserEditViewModel model)
        {
            var dataMapped = _mapper.Map<ApplicationUserEditViewModel, ApplicationUser>(model);
            var identityResult = await _userManager.UpdateAsync(dataMapped);
            if (identityResult.Succeeded == true)
            {
                return AppResult.Ok(MessageHelper.Update);
            }
            else
            {
                return AppResult.Fail(MessageHelper.UpdateFail);
            }
        }
        
        public async Task<AppResult> DeleteApplicationUserAsync(string key, CancellationToken cancellationToken = default)
        {
            var dataMapped = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == key);
            var identityResult = await _userManager.DeleteAsync(dataMapped);
            if (identityResult.Succeeded == true)
            {
                return AppResult.Ok(MessageHelper.Delete);
            }
            else
            {
                return AppResult.Fail(MessageHelper.DeleteFail);
            }
        }
        
        public async Task<List<SelectListItem>> GetApplicationUserDropDownViewModelAsync(bool isActive)
        {
            var data = await this.GetApplicationUsersAsync(isActive);
            var dropDownViewModel = SelectListItemExtension.PopulateDropdownList(data, "Id", "Name");
            return dropDownViewModel;
        }

        public async Task<ApplicationUserViewModel> GetApplicationUserByUserNameOrEmailAsync(string userNameOrEmail)
        {
            var data = await _userManager.Users.FirstOrDefaultAsync(x => (x.UserName == userNameOrEmail || x.Email == userNameOrEmail) && x.IsActive == true && x.IsDeleted != true);

            var dataRoles = await _userManager.GetRolesAsync(data);

            var pp = _mapper.Map<ApplicationUser, ApplicationUserViewModel>(data);
            pp.RoleIds = dataRoles;

            return pp;
        }

        public async Task<AppResult> IsInRoleAsync(ApplicationUserViewModel model, string roleName)
        {
            var dataMapped = _mapper.Map<ApplicationUserViewModel, ApplicationUser>(model);
            var isAdmin = await _userManager.IsInRoleAsync(dataMapped, roleName);

            if (isAdmin == true)
            {
                return AppResult.Ok(MessageHelper.AlreadyExists);
            }
            else
            {
                return AppResult.Fail(MessageHelper.Fail);
            }
        }

        public async Task<AppResult> CreateApplicationUserWithRoleAsync(RegisterViewModel model, CancellationToken cancellationToken = default)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var identityResult = await _userManager.CreateAsync(user, model.Password);
            if (identityResult.Succeeded == true)
            {
                var role = new ApplicationRole
                {
                    Id = model.RoleName,
                    Name = model.RoleName,
                    IsActive = true
                };
                await _userManager.AddToRoleAsync(user, role.Id);

                return AppResult.Ok(MessageHelper.Save);
            }
            else
            {
                return AppResult.Fail(MessageHelper.SaveFail);
            }
        }

        public async Task<AppResult> IsEmailConfirmedAsync(ApplicationUserViewModel model)
        {
            var dataMapped = _mapper.Map<ApplicationUserViewModel, ApplicationUser>(model);
            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(dataMapped);

            if (isEmailConfirmed == true)
            {
                return AppResult.Ok(MessageHelper.AlreadyExists);
            }
            else
            {
                return AppResult.Fail(MessageHelper.Fail);
            }
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUserViewModel model)
        {
            var dataMapped = _mapper.Map<ApplicationUserViewModel, ApplicationUser>(model);
            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(dataMapped);
            return passwordResetToken;
        }

        public async Task<AppResult> ResetPasswordAsync(ApplicationUserViewModel model, string token, string password)
        {
            var dataMapped = _mapper.Map<ApplicationUserViewModel, ApplicationUser>(model);
            var identityResult = await _userManager.ResetPasswordAsync(dataMapped, token, password);
            if (identityResult.Succeeded == true)
            {
                return AppResult.Ok(MessageHelper.Success);
            }
            else
            {
                return AppResult.Fail(MessageHelper.Fail);
            }
        }
    }
}
