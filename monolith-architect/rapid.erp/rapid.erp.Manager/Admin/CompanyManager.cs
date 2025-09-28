using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using rapid.erp.Common;
using rapid.erp.Core.Extensions;
using rapid.erp.Core.Helpers;
using rapid.erp.Core.Utility;
using rapid.erp.EntityModel.Admin;
using rapid.erp.IManager;
using rapid.erp.IRepository;
using rapid.erp.ViewModel.Admin;
using rapid.erp.ViewModel.Common;

namespace rapid.erp.Manager
{
    public class CompanyManager : ICompanyManager
    {
        private ICompanyRepository _iCompanyRepository;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public CompanyManager(IMapper mapper, IHttpContextAccessor httpContextAccessor, ICompanyRepository iCompanyRepository)
        {
            _mapper = mapper;
            _iCompanyRepository = iCompanyRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<CompanyViewModel>> GetCompanysAsync()
        {
            var data = await _iCompanyRepository.GetCompanysAsync(CancellationToken.None);
            var pp = _mapper.Map<List<Company>, List<CompanyViewModel>>(data);
            return pp;
        }
        
        public async Task<CompanyViewModel> GetCompanyAsync(int key)
        {
            var data = await _iCompanyRepository.GetCompanyAsync(key);
            var pp = _mapper.Map<Company, CompanyViewModel>(data);
            return pp;
        }

        public async Task<CompanyViewModel> GetCompanyByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var data = await _iCompanyRepository.GetCompanyByNameAsync(name, cancellationToken);
            var pp = _mapper.Map<Company, CompanyViewModel>(data);
            return pp;
        }

        public async Task<CompanyEditViewModel> GetCompanyForEditAsync(int key)
        {
            var data = await _iCompanyRepository.GetCompanyAsync(key);
            var pp = _mapper.Map<Company, CompanyEditViewModel>(data);
            return pp;
        }

        public async Task<List<CompanyViewModel>> GetCompanysAsync(bool isActive, CancellationToken cancellationToken = default)
        {
            var data = await _iCompanyRepository.GetCompanysAsync(isActive, cancellationToken);
            var pp = _mapper.Map<List<Company>, List<CompanyViewModel>>(data);
            return pp;
        }

        public async Task<SearchResult<IEnumerable<CompanyViewModel>>> GetCompanysSearchResultAsync(SearchModel searchModel, bool pagination)
        {
            var data = await _iCompanyRepository.GetCompanysSearchResultAsync(searchModel, pagination);
            var dataMapped = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(data.Value);
            var result = new SearchResult<IEnumerable<CompanyViewModel>>(dataMapped, data.TotalRows, data.PageRows, true, "");
            return result;
        }
        
        public async Task<IEnumerable<CompanyExportViewModel>> GetCompanysExportResultAsync(SearchModel searchModel)
        {
            var data = await _iCompanyRepository.GetCompanysExportResultAsync(searchModel);
            var dataMapped = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyExportViewModel>>(data);
            return dataMapped;
        }
        
        public async Task<DefaultPageViewModel<CompanyCreateViewModel, SearchModel, SearchResult<IEnumerable<CompanyViewModel>>>> GetCompanysDefaultPageViewModelAsync(SearchModel searchModel)
        {
            var resultData = await GetCompanysSearchResultAsync(searchModel, true);
            var returnModel = new DefaultPageViewModel<CompanyCreateViewModel, SearchModel, SearchResult<IEnumerable<CompanyViewModel>>>()
            {
                CreateorEditViewModel = new CompanyCreateViewModel(),
                SearchOptionViewModel = searchModel,
                SearchResultViewModel = resultData
            };
            return returnModel;
        }
        
        public async Task<DefaultPageViewModel<CompanyCreateViewModel, SearchModel, SearchResult<IEnumerable<CompanyViewModel>>>> GetCompanysDefaultPageViewModelAsync(SearchModel searchModel, bool pagination)
        {
            var resultData = await GetCompanysSearchResultAsync(searchModel, pagination);
            var returnModel = new DefaultPageViewModel<CompanyCreateViewModel, SearchModel, SearchResult<IEnumerable<CompanyViewModel>>>()
            {
                CreateorEditViewModel = new CompanyCreateViewModel(),
                SearchOptionViewModel = searchModel,
                SearchResultViewModel = resultData
            };
            return returnModel;
        }
        
        public async Task<AppResult> CreateCompanyAsync(CompanyCreateViewModel model, CancellationToken cancellationToken = default)
        {
            var dataMapped = _mapper.Map<CompanyCreateViewModel, Company>(model);
            var data = await _iCompanyRepository.CreateCompanyAsync(dataMapped);
            return data;
        }

        public async Task<AppResult> UpdateCompanyAsync(CompanyEditViewModel model)
        {
            var dataMapped = _mapper.Map<CompanyEditViewModel, Company>(model);
            var data = await _iCompanyRepository.UpdateCompanyAsync(dataMapped);
            return data;
        }
        
        public async Task<AppResult> DeleteCompanyAsync(int key, CancellationToken cancellationToken = default)
        {
            var data = await _iCompanyRepository.DeleteCompanyAsync(key);
            return data;
        }
        
        public async Task<List<SelectListItem>> GetCompanyDropDownViewModelAsync(bool isActive)
        {
            var data = await _iCompanyRepository.GetCompanysAsync(isActive);
            var dropDownViewModel = SelectListItemExtension.PopulateDropdownList(data, "CompanyId", "NameEnglish");
            return dropDownViewModel;
        }

        public async Task<AppResult> GetTypeAheadAsync(string query)
        {

            var viewList = await _iCompanyRepository.GetTypeAheadAsync(query, CancellationToken.None);

            if (viewList.Any())
            {
                var dataList = viewList.Select(s => new TypeAheadViewModel() { Id = s.CompanyId, Name = s.NameEnglish }).ToList();

                return AppResult.Ok(MessageHelper.DataFound, parentId: 0, parentName: "", data: dataList);
            }
            else
            {
                return AppResult.Fail(MessageHelper.DataNotFound);
            }
        }

        public async Task<AppResult> CreateOrUpdateCompanyAsync(CompanyViewModel model, CancellationToken cancellationToken = default)
        {
            var result = new AppResult();

            var dataMapped = _mapper.Map<CompanyViewModel, Company>(model);
            //add
            if (model.CompanyId == 0)
            {
                result = await _iCompanyRepository.CreateCompanyAsync(dataMapped);
            }
            else if (model.CompanyId > 0) //edit
            {
                result = await _iCompanyRepository.CreateCompanyAsync(dataMapped);
            }
            
            return result;
        }
    }
}
