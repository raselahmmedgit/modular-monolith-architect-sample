using rapid.erp.Common;
using rapid.erp.Core.Utility;
using rapid.erp.EntityModel.Admin;

namespace rapid.erp.IRepository
{
    public interface ICompanyRepository
    {
        IQueryable<Company> GetCompanysQuery();
        Task<Company> GetCompanyAsync(int key, CancellationToken cancellationToken = default);
        Task<List<Company>> GetCompanysAsync(CancellationToken cancellationToken);
        Task<List<Company>> GetCompanysAsync(bool isActive, CancellationToken cancellationToken = default);
        [Obsolete]
        Task<SearchResult<IEnumerable<Company>>> GetCompanysSearchResultAsync(SearchModel searchModel, bool pagination, CancellationToken cancellationToken = default);

        Task<SearchResult<IEnumerable<Company>>> GetCompanysSearchResultAsync(SearchModel searchModel, CancellationToken cancellationToken = default);
        Task<AppResult> CreateCompanyAsync(Company model, CancellationToken cancellationToken = default);
        Task<AppResult> UpdateCompanyAsync(Company model, CancellationToken cancellationToken = default);
        Task<AppResult> DeleteCompanyAsync(int key, CancellationToken cancellationToken = default);
        Task<IEnumerable<Company>> GetCompanysExportResultAsync(SearchModel searchModel, CancellationToken cancellationToken = default);
        Task<IEnumerable<Company>> GetTypeAheadAsync(string searchText, CancellationToken cancellationToken = default);
        Task<Company> GetCompanyByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
