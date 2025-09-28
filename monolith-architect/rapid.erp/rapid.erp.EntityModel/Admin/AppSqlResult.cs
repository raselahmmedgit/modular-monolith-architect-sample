using System.ComponentModel.DataAnnotations;

namespace rapid.erp.EntityModel.Admin
{
    public class AppSqlResult
    {
        [Key]
        public int ResultId { get; set; }

        public bool Success { get; set; }

        public string? Message { get; set; }

        public string? MessageType { get; }

        public int ParentId { get; }

        public int ResultCount { get; set; }

        public string? RedirectUrl { get; }
    }
}
