using System.ComponentModel.DataAnnotations;

namespace rapid.erp.EntityModel.Security
{
    public class AppMenuType
    {
        public AppMenuType()
        {
            Menus = new HashSet<AppMenu>();
            SubMenus = new HashSet<AppSubMenu>();
        }

        [Key]
        public int MenuTypeId { get; set; }
        [StringLength(256)]
        public string MenuTypeName { get; set; }

        public virtual ICollection<AppMenu> Menus { get; set; }

        public virtual ICollection<AppSubMenu> SubMenus { get; set; }
    }
}