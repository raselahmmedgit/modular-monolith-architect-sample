using Microsoft.EntityFrameworkCore;
using rapid.erp.Common;
using rapid.erp.Core.Helpers;
using rapid.erp.Core.Utility;
using rapid.erp.EntityModel;
using rapid.erp.EntityModel.Admin;
using rapid.erp.IRepository;
using rapid.erp.Repository.Extensions;

namespace rapid.erp.Repository
{
    public class CompanyRepository : RepositoryBase, ICompanyRepository
    {
        private readonly AppDbContext _context;
        public CompanyRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }

        public IQueryable<Company> GetCompanysQuery()
        {
            return _context.Company.AsQueryable();
        }
        
        public async Task<List<Company>> GetCompanysAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested == false)
            {
                var data = await _context.Company.ToListAsync();
                return data;
            }
            return null;
        }

        public async Task<List<Company>> GetCompanysAsync(bool isActive, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested == false)
            {
                var data = await _context.Company.Where(x => x.IsActive == isActive && x.IsDeleted != true).ToListAsync();
                return data;
            }
            return null;
        }

        public async Task<Company> GetCompanyAsync(int key, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested == false)
            {
                var data = await _context.Company.FirstOrDefaultAsync(x => x.CompanyId == key && x.IsDeleted != true);
                return data;
            }
            return null;
        }

        public async Task<Company> GetCompanyByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested == false)
            {
                var data = await _context.Company.FirstOrDefaultAsync(x => (x.NameEnglish == name || x.NameArabic == name) && x.IsActive == true && x.IsDeleted != true);
                return data;
            }
            return null;
        }

        public async Task<List<Company>> GetCompanyModelByCompanyIdsAsync(List<int> ids, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested == false)
            {
                return await _context.Company.Where(x => ids.Contains(x.CompanyId)).ToListAsync();
            }
            return null;
        }
        
        public async Task<SearchResult<IEnumerable<Company>>> GetCompanysSearchResultAsync(SearchModel searchModel, bool pagination, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested == false)
            {
                string? st = searchModel.SearchText?.Trim().ToLower();
                var query = _context.Company.Where(x => x.IsDeleted != true).AsQueryable();
                if (!string.IsNullOrEmpty(searchModel.SearchText))
                {
                    query = query.Where(x => x.NameEnglish.ToLower().Contains(st)
                     || x.NameArabic.ToLower().Contains(st)
                     || x.ShortEnglish.ToLower().Contains(st)
                     || x.ShortArabic.ToLower().StartsWith(st));
                }
                var totalCount = await query.CountAsync();
                var dataQuery = query.OrderByName(searchModel.SortColumn, searchModel.IsDescending);
                if (pagination == true)
                {
                    dataQuery = dataQuery.Skip((searchModel.GetCurrentPage() - 1) * (searchModel.Rows)).Take(searchModel.Rows);
                }
                var data = await dataQuery.ToListAsync();
                return new SearchResult<IEnumerable<Company>>(data, totalCount, data.Count, true, "");
            }
            return null;
        }
        
        public async Task<SearchResult<IEnumerable<Company>>> GetCompanysSearchResultAsync(SearchModel searchModel, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested == false)
            {
                string? st = searchModel.SearchText?.Trim().ToLower();
                var query = _context.Company.Where(x => x.IsDeleted != true).AsQueryable();
                if (!string.IsNullOrEmpty(searchModel.SearchText))
                {
                    query = query.Where(x => x.NameEnglish.ToLower().Contains(st)
                     || x.NameArabic.ToLower().Contains(st)
                     || x.ShortEnglish.ToLower().Contains(st)
                     || x.ShortArabic.ToLower().StartsWith(st));
                }
                var count = await query.CountAsync();
                var dataQuery = query.OrderByName(searchModel.SortColumn, searchModel.IsDescending)
                    .Skip((searchModel.GetCurrentPage() - 1) * (searchModel.Rows)).Take(searchModel.Rows);

                var data = await dataQuery.ToListAsync();
                return new SearchResult<IEnumerable<Company>>(data, count, data.Count, true, "");
            }
            return null;
        }

        public async Task<IEnumerable<Company>> GetCompanysExportResultAsync(SearchModel searchModel, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested == false)
            {
                string st = searchModel.SearchText?.Trim().ToLower();

                var query = _context.Company.Where(x => x.IsDeleted != true).AsQueryable();
                if (!string.IsNullOrEmpty(searchModel.SearchText))
                {
                    query = query.Where(x => x.NameEnglish.ToLower().Contains(st)
                     || x.NameArabic.ToLower().Contains(st)
                     || x.ShortEnglish.ToLower().Contains(st)
                     || x.ShortArabic.ToLower().StartsWith(st));
                }
                var dataQuery = query.OrderByName(searchModel.SortColumn, searchModel.IsDescending);
                var data = await dataQuery.ToListAsync();
                return data;
            }
            return null;
        }

        public async Task<AppResult> CreateCompanyAsync(Company model, CancellationToken cancellationToken = default)
        {
            int count = 0;
            if (cancellationToken.IsCancellationRequested == false)
            {
                var exists = await _context.Company.AnyAsync(
                    x => (x.NameEnglish.Trim().ToLower() == model.NameEnglish.Trim().ToLower() || x.NameArabic.Trim().ToLower() == model.NameArabic.Trim().ToLower()) && x.IsDeleted != true);
                if (exists == false)
                {
                    await _context.Company.AddAsync(model, cancellationToken);
                    count = await _context.SaveChangesAsync(cancellationToken);
                    return AppResult.Ok(MessageHelper.Save);
                }
                else
                {
                    return AppResult.Fail(MessageHelper.AlreadyExists);
                }
            }
            return AppResult.Fail(MessageHelper.SaveFail);
        }

        public async Task<AppResult> UpdateCompanyAsync(Company model, CancellationToken cancellationToken = default)
        {
            int count = 0;
            if (cancellationToken.IsCancellationRequested == false)
            {
                var exists = await _context.Company.AnyAsync(
                   x => (x.NameEnglish.Trim().ToLower() == model.NameEnglish.Trim().ToLower() || x.NameArabic.Trim().ToLower() == model.NameArabic.Trim().ToLower()) && x.IsDeleted != true && x.CompanyId != model.CompanyId);

                if (exists == false)
                {
                    var original = await _context.Company.FirstOrDefaultAsync(x => x.CompanyId == model.CompanyId);

                    _context.Entry<Company>(original).CurrentValues.SetValues(model);

                    count = await _context.SaveChangesAsync(cancellationToken);
                    return AppResult.Ok(MessageHelper.Update);
                }
                else
                {
                    return AppResult.Fail(MessageHelper.AlreadyExists);
                }
            }
            return AppResult.Fail(MessageHelper.UpdateFail);
        }

        public async Task<AppResult> DeleteCompanyAsync(int key, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested == false)
            {
                var exists = await _context.Company.FirstOrDefaultAsync(
                    x => x.CompanyId == key);
                if (exists != null)
                {
                    return await DeleteOrVarifyRecordAsync<Company>(key, true);
                }
                else
                {
                    return AppResult.Fail(MessageHelper.NullError);
                }
            }
            return AppResult.Fail(MessageHelper.DeleteFail);
        }

        public async Task<IEnumerable<Company>> GetTypeAheadAsync(string searchText, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested == false)
            {
                searchText = string.IsNullOrEmpty(searchText) ? "" : searchText.Trim().ToLower();
                var query = _context.Company.Where(x => x.IsDeleted != true).AsQueryable();
                if (!string.IsNullOrEmpty(searchText))
                {
                    query = query.Where(x => (x.NameEnglish.Trim().ToLower().Contains(searchText) || x.NameArabic.Trim().ToLower().Contains(searchText)));
                }

                var data = await query.ToListAsync();

                return data;
            }
            return null;
        }
    }
}
