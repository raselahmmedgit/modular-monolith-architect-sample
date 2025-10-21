using System.ComponentModel.DataAnnotations;

namespace rapid.erp.EntityModel.Admin
{
    public class AppModule
    {
        public AppModule()
        {
            SubModules = new HashSet<AppSubModule>();
            Menus = new HashSet<AppMenu>();
        }

        [Key]
        public Guid ModuleId { get; set; } = Guid.NewGuid();
        [StringLength(256)]
        public string ModuleName { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string AreaName { get; set; }
        public string IconPath { get; set; }
        public bool? HasChild { get; set; }
        public int? ModuleDisplayOrder { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
        public string? DeleteReason { get; set; }

        public virtual ICollection<AppSubModule> SubModules { get; set; }

        public virtual ICollection<AppMenu> Menus { get; set; }
    }
}