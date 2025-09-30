using Microsoft.AspNetCore.Identity;

namespace rapid.erp.Core.Security
{
    /// <summary>
    /// Application Role custom model. Extended from Identity Role.
    /// </summary>
    public class ApplicationRole : IdentityRole<string>
    {
        
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
        public string? DeleteReason { get; set; }
    }
}
