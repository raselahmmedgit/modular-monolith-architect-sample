using System.ComponentModel.DataAnnotations;

namespace rapid.erp.EntityModel.Security
{
    public class AppUserTokenRefresh : IBaseEntityModel, IChangeTrackerEntity, IDeleteTrackerEntity
    {
        [Key]
        public int UserTokenRefreshId { get; set; }
        public string? UserId { get; set; }
        public string? DeviceId { get; set; }
        public string? TokenHash { get; set; }
        public string? TokenSalt { get; set; }
        public DateTime? TimeStamp { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? RefreshToken { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
        public string? DeleteReason { get; set; }
    }
}
