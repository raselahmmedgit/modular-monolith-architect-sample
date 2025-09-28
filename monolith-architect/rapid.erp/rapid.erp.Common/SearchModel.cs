using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace rapid.erp.Common
{
    public class SearchModel<TValue> : SearchModel
    {
        public TValue Value { get; set; }
        public SearchModel(TValue value)
        {
            Page = 1;
            Rows = 15;
            Value = value;
        }
    }

    public class SearchModel
    {
        public SearchModel()
        {
            Page = 1;
            Rows = 10;
            ShowPageSizes = true;
        }

        public int Id { get; set; }
        public string StrId { get; set; }

        public long ApplicationId { get; set; }
        public long CompanyId { get; set; }
        public long CountryId { get; set; }
        public long DivisionId { get; set; }
        public long DistrictId { get; set; }
        public long UpazilaId { get; set; }
        public long CityId { get; set; }
        public long BranchId { get; set; }
        public long ProjectId { get; set; }
        
        public int Year { get; set; }
        public int Month { get; set; }

        public int? LoginUserId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public int ComponentGroupId { get; set; }
        public string ComponentKey { get; set; }

        public bool IsCancel { get; set; }
        public bool IsSelected { get; set; }

        public string SearchText { get; set; }

        public int PageTitleId { get; set; }

        public string PageTitle { get; set; }

        public int PageSubTitleId { get; set; }

        public string PageSubTitle { get; set; }

        public IDictionary SerchKeyAndValue { get; set; }
        
        public int Page { get; set; }
        
        public int Rows { get; set; }
        
        public bool ShowPageSizes { get; set; }
        
        public string Sort { get; set; }
        
        public string SortDirection
        {
            get
            {
                if (!string.IsNullOrEmpty(Sort))
                {
                    Sort = Sort.Replace("+", " ");
                    var d = Sort.Split(' ').ToList();
                    if(d != null && d.Count > 0)
                        return Sort.Split(' ').ToList().Skip(1).First().ToLower();
                    return "desc";
                }
                else
                {
                    //Sort = "CreatedDate desc";
                    //return Sort.Split(' ').ToList().Skip(1).First().ToLower();
                    return "desc";
                }
            }
        }
        
        public string SortColumn
        {
            get
            {
                if (!string.IsNullOrEmpty(Sort))
                {
                    var _sort = Sort.Replace("+", " "); 
                    return _sort.Split(' ').ToList().First();
                }
                return "CreatedDate";
            }
        }

        public string GetSortColumn(string defaultValue = "")
        {
            if (!string.IsNullOrEmpty(Sort))
            {
                var _sort = Sort.Replace("+", " ");
                return _sort.Split(' ').ToList().First();
            }
            return defaultValue;
        }

        public int TotalCount { get; set; }
        
        public bool IsDescending
        {
            get
            {
                return SortDirection == "desc";
            }
        }
        
        public Dictionary<int, string> PageSizes => new() { { 10, "10" }, { 15, "15" }, { 20, "20" }, { 50, "50" }, { 100, "100" } };
       
        public int GetCurrentPage()
        {
            if (Page < 1)
                return 1;
            return Page;
        }

        public string RequestedUrl { get; set; }
        public string RequestedUrlQueryKey { get; set; }

        public DateTime? StartDate { get; set; }
        public string StartDateStr { get; set; }
        public DateTime? EndDate { get; set; }
        public string EndDateStr { get; set; }

        public string CurrentVersion { get; set; }
        public int? CurrentVersionNumber { get; set; }

        public string ReturnUrl { get; set; }
        public string ReturnUrlQueryKey { get; set; }

        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanView { get; set; }
        public bool CanDelete { get; set; }
    }
}
