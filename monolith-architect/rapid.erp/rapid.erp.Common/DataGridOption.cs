using System.Linq;

namespace rapid.erp.Common
{
    public class DataGridOption
    {
        public DataGridOption()
        {
            Page = 1;
            Rows = 15;
        }
        public int Page { get; set; }
        public int Rows { get; set; }
        public string Sort { get; set; }
        public string SortOrder
        {
            get
            {
                if (!string.IsNullOrEmpty(Sort))
                {
                    return Sort.Split(' ').ToList().Skip(1).First();
                }
                return "";
            }
        }
        public string SortColumn
        {
            get
            {
                if (!string.IsNullOrEmpty(Sort))
                {
                    return Sort.Split(' ').ToList().First();
                }
                return "";
            }
        }
        public int TotalCount { get; set; }

    }
}
