using Microsoft.AspNetCore.Identity;

namespace rapid.erp.Core.Security
{
    /// <summary>
    /// Application User custom model. Extended from Identity User.
    /// </summary>
    public class ApplicationUser : IdentityUser<string>
    {
        [PersonalData]
        public int AppUserId { get; set; }

        [PersonalData]
        public string? FirstName { get; set; }

        [PersonalData]
        public string? LastName { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
        public string? DeleteReason { get; set; }

    }
}
