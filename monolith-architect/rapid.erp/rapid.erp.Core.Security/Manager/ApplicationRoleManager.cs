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
    public class ApplicationRoleManager : IApplicationRoleManager
    {
        private RoleManager<ApplicationRole> _roleManager;
        private IMapper _mapper;

        public ApplicationRoleManager(IMapper mapper, RoleManager<ApplicationRole> roleManager)
        {
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<List<ApplicationRoleViewModel>> GetApplicationRolesAsync()
        {
            var data = await _roleManager.Roles.ToListAsync();
            var pp = _mapper.Map<List<ApplicationRole>, List<ApplicationRoleViewModel>>(data);
            return pp;
        }
        
        public async Task<ApplicationRoleViewModel> GetApplicationRoleAsync(string key)
        {
            var data = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == key);
            var pp = _mapper.Map<ApplicationRole, ApplicationRoleViewModel>(data);
            return pp;
        }

        public async Task<ApplicationRoleEditViewModel> GetApplicationRoleForEditAsync(string key)
        {
            var data = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == key);
            var pp = _mapper.Map<ApplicationRole, ApplicationRoleEditViewModel>(data);
            return pp;
        }

        public async Task<List<ApplicationRoleViewModel>> GetApplicationRolesAsync(bool isActive, CancellationToken cancellationToken = default)
        {
            var data = await _roleManager.Roles.Where(x => x.IsActive == isActive).ToListAsync(cancellationToken);
            var pp = _mapper.Map<List<ApplicationRole>, List<ApplicationRoleViewModel>>(data);
            return pp;
        }

        public async Task<SearchResult<IEnumerable<ApplicationRoleViewModel>>> GetApplicationRolesSearchResultAsync(SearchModel searchModel, bool pagination)
        {
            string? st = searchModel.SearchText?.Trim().ToLower();
            var query = _roleManager.Roles.Where(x => x.IsDeleted != true).AsQueryable();
            if (!string.IsNullOrEmpty(searchModel.SearchText))
            {
                query = query.Where(x => x.Name.ToLower().Contains(st)
                 || x.NormalizedName.ToLower().StartsWith(st));
            }
            var totalCount = await query.CountAsync();
            var dataQuery = query.OrderByName(searchModel.SortColumn, searchModel.IsDescending);
            if (pagination == true)
            {
                dataQuery = dataQuery.Skip((searchModel.GetCurrentPage() - 1) * (searchModel.Rows)).Take(searchModel.Rows);
            }
            
            var dataList = await dataQuery.ToListAsync();
            var dataCount = dataList.Count;
            var data = new SearchResult<IEnumerable<ApplicationRole>>(dataList, totalCount, dataCount, true, "");

            var dataMapped = _mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(data.Value);
            var result = new SearchResult<IEnumerable<ApplicationRoleViewModel>>(dataMapped, data.TotalRows, data.PageRows, true, "");
            return result;
        }
        
        public async Task<DefaultPageViewModel<ApplicationRoleCreateViewModel, SearchModel, SearchResult<IEnumerable<ApplicationRoleViewModel>>>> GetApplicationRolesDefaultPageViewModelAsync(SearchModel searchModel)
        {
            var resultData = await GetApplicationRolesSearchResultAsync(searchModel, true);
            var returnModel = new DefaultPageViewModel<ApplicationRoleCreateViewModel, SearchModel, SearchResult<IEnumerable<ApplicationRoleViewModel>>>()
            {
                CreateorEditViewModel = new ApplicationRoleCreateViewModel(),
                SearchOptionViewModel = searchModel,
                SearchResultViewModel = resultData
            };
            return returnModel;
        }
        
        public async Task<DefaultPageViewModel<ApplicationRoleCreateViewModel, SearchModel, SearchResult<IEnumerable<ApplicationRoleViewModel>>>> GetApplicationRolesDefaultPageViewModelAsync(SearchModel searchModel, bool pagination)
        {
            var resultData = await GetApplicationRolesSearchResultAsync(searchModel, pagination);
            var returnModel = new DefaultPageViewModel<ApplicationRoleCreateViewModel, SearchModel, SearchResult<IEnumerable<ApplicationRoleViewModel>>>()
            {
                CreateorEditViewModel = new ApplicationRoleCreateViewModel(),
                SearchOptionViewModel = searchModel,
                SearchResultViewModel = resultData
            };
            return returnModel;
        }
        
        public async Task<AppResult> CreateApplicationRoleAsync(ApplicationRoleCreateViewModel model, CancellationToken cancellationToken = default)
        {
            var dataMapped = _mapper.Map<ApplicationRoleCreateViewModel, ApplicationRole>(model);
            var identityResult = await _roleManager.CreateAsync(dataMapped);
            if (identityResult.Succeeded == true)
            {
                return AppResult.Ok(MessageHelper.Save);
            }
            else
            {
                return AppResult.Fail(MessageHelper.SaveFail);
            }
        }

        public async Task<AppResult> UpdateApplicationRoleAsync(ApplicationRoleEditViewModel model)
        {
            var dataMapped = _mapper.Map<ApplicationRoleEditViewModel, ApplicationRole>(model);
            var identityResult = await _roleManager.UpdateAsync(dataMapped);
            if (identityResult.Succeeded == true)
            {
                return AppResult.Ok(MessageHelper.Update);
            }
            else
            {
                return AppResult.Fail(MessageHelper.UpdateFail);
            }
        }
        
        public async Task<AppResult> DeleteApplicationRoleAsync(string key, CancellationToken cancellationToken = default)
        {
            var dataMapped = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == key);
            var identityResult = await _roleManager.DeleteAsync(dataMapped);
            if (identityResult.Succeeded == true)
            {
                return AppResult.Ok(MessageHelper.Delete);
            }
            else
            {
                return AppResult.Fail(MessageHelper.DeleteFail);
            }
        }
        
        public async Task<List<SelectListItem>> GetApplicationRoleDropDownViewModelAsync(bool isActive)
        {
            var data = await this.GetApplicationRolesAsync(isActive);
            var dropDownViewModel = SelectListItemExtension.PopulateDropdownList(data, "Id", "Name");
            return dropDownViewModel;
        }

    }
}
