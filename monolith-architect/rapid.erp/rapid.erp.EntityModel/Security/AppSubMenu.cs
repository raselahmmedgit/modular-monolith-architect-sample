using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rapid.erp.EntityModel.Security
{
    public class AppSubMenu : IBaseEntityModel, IChangeTrackerEntity, IDeleteTrackerEntity
    {
        [Key]
        public int SubMenuId { get; set; }
        [StringLength(256)]
        public string SubMenuName { get; set; }
        public int? MenuId { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string AreaName { get; set; }
        public string DefaultQuery { get; set; }
        public string IconPath { get; set; }
        public bool? HasChild { get; set; }
        public int? DisplayOrder { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
        public string? DeleteReason { get; set; }


        [ForeignKey("MenuId")]
        public virtual AppMenu Menu { get; set; }
    }
}