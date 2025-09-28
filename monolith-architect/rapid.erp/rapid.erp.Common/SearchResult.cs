using rapid.erp.Core.Utility;

namespace rapid.erp.Common
{
    public class SearchResult<TValue>
    {
        public TValue Value { get; set; }
        public int TotalRows { get; set; }
        public int PageRows { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public SearchResult(TValue value, int totalRows, bool success, string error)
        {
            TotalRows = totalRows;
            Value = value;

        }
        public SearchResult(TValue value, int totalRows, int pageRows, bool success = false, string error = "")
        {
            TotalRows = totalRows;
            PageRows = pageRows;
            Value = value;
        }
    }
}
