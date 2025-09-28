using System.ComponentModel.DataAnnotations;

namespace rapid.erp.EntityModel.Security
{
    public class AppSubModule : IBaseEntityModel, IChangeTrackerEntity, IDeleteTrackerEntity
    {
        public AppSubModule()
        {
            Menus = new HashSet<AppMenu>();
        }

        [Key]
        public int SubModuleId { get; set; }
        [StringLength(256)]
        public string SubModuleName { get; set; }
        public int ModuleId { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string AreaName { get; set; }
        public string IconPath { get; set; }
        public bool? HasChild { get; set; }
        public int? GroupDisplayOrder { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
        public string? DeleteReason { get; set; }

        public virtual ICollection<AppMenu> Menus { get; set; }
    }
}