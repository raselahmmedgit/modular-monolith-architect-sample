using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rapid.erp.EntityModel.Admin
{
    public class AppMenu : IBaseEntityModel, IChangeTrackerEntity, IDeleteTrackerEntity
    {
        [Key]
        public Guid MenuId { get; set; } = Guid.NewGuid();
        [StringLength(256)]
        public string MenuName { get; set; }
        public int? ModuleId { get; set; }
        public int? SubModuleId { get; set; }
        public int? MenuTypeId { get; set; }
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


        [ForeignKey("ModuleId")]
        public virtual AppModule Module { get; set; }

        [ForeignKey("SubModuleId")]
        public virtual AppSubModule SubModule { get; set; }

        [ForeignKey("MenuTypeId")]
        public virtual AppMenuType MenuType { get; set; }
    }
}