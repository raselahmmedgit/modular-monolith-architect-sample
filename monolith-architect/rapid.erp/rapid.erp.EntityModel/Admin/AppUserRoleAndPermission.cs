using System.ComponentModel.DataAnnotations;

namespace rapid.erp.EntityModel.Admin
{
    public class AppUserRoleAndPermission : IBaseEntityModel, IChangeTrackerEntity, IDeleteTrackerEntity
    {
        [Key]
        public int UserRoleAndPermissionId { get; set; }
        public string RoleId { get; set; }
        public int? MenuId { get; set; }
        public int? SubMenuId { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanApprove { get; set; }

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