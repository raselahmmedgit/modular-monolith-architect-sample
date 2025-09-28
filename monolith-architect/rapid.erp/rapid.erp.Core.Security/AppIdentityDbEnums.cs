using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace rapid.erp.ViewModel.Database
{
    public static class AppIdentityDbEnums
    {
        public enum ApplicationRoleEnum
        {
            [Display(Name = "SuperAdmin")]
            [Description("bef1229d-29ce-4468-be4d-0e10be0e0102")]
            SuperAdmin = 1,
            [Display(Name = "Admin")]
            [Description("2ac48615-4620-476b-899f-d99aa3138f4b")]
            Admin = 2,
            [Display(Name = "User")]
            [Description("1e500ab8-4bd1-4dee-a7e4-d4f7c7a45b28")]
            User = 3
        }

        public enum ApplicationUserEnum
        {
            [Display(Name = "SuperAdmin")]
            [Description("f8d45ddf-cc95-4c6d-9de5-3849ccff5777")]
            SuperAdmin = 1,
            [Display(Name = "Admin")]
            [Description("fb3d9803-5bb7-4a48-bbd2-f63038739365")]
            Admin = 2,
            [Display(Name = "User")]
            [Description("3ce11982-3ff7-46a9-a450-038eb42a3849")]
            User = 3
        }
    }
}
