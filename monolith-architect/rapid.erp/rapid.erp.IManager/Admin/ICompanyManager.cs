using Microsoft.AspNetCore.Mvc.Rendering;
using rapid.erp.Common;
using rapid.erp.Core.Utility;
using rapid.erp.ViewModel.Admin;

namespace rapid.erp.IManager
{
    public interface ICompanyManager
    {
        Task<List<CompanyViewModel>> GetCompanysAsync();

        Task<CompanyViewModel> GetCompanyAsync(int key);

        Task<List<CompanyViewModel>> GetCompanysAsync(bool isActive, CancellationToken cancellationToken = default);

        Task<CompanyEditViewModel> GetCompanyForEditAsync(int key);

        Task<AppResult> UpdateCompanyAsync(CompanyEditViewModel model);

        Task<SearchResult<IEnumerable<CompanyViewModel>>> GetCompanysSearchResultAsync(SearchModel searchModel, bool pagination);

        Task<DefaultPageViewModel<CompanyCreateViewModel, SearchModel, SearchResult<IEnumerable<CompanyViewModel>>>> GetCompanysDefaultPageViewModelAsync(SearchModel searchModel);

        Task<DefaultPageViewModel<CompanyCreateViewModel, SearchModel, SearchResult<IEnumerable<CompanyViewModel>>>> GetCompanysDefaultPageViewModelAsync(SearchModel searchModel, bool pagination);

        Task<IEnumerable<CompanyExportViewModel>> GetCompanysExportResultAsync(SearchModel searchModel);

        Task<AppResult> CreateCompanyAsync(CompanyCreateViewModel model, CancellationToken cancellationToken = default);

        Task<AppResult> DeleteCompanyAsync(int key, CancellationToken cancellationToken = default);

        Task<List<SelectListItem>> GetCompanyDropDownViewModelAsync(bool isActive);

        Task<AppResult> GetTypeAheadAsync(string query);

        Task<CompanyViewModel> GetCompanyByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<AppResult> CreateOrUpdateCompanyAsync(CompanyViewModel model, CancellationToken cancellationToken = default);
    }
}
