using Microsoft.EntityFrameworkCore;
using rapid.erp.Core.Extensions;
using rapid.erp.Core.Security.EntityModel;
using rapid.erp.ViewModel.Database;

namespace rapid.erp.Core.Security
{
    public static class AppIdentityDbContextSeedData
    {
        public static void SeedData(ModelBuilder builder)
        {

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = AppIdentityDbEnums.ApplicationRoleEnum.SuperAdmin.ToDescriptionAttr(), Name = "SuperAdmin", NormalizedName = "SuperAdmin", IsActive = true, CreatedBy = AppIdentityDbEnums.ApplicationUserEnum.SuperAdmin.ToString(), CreatedDate = DateTime.UtcNow, UpdatedBy = null, UpdatedDate = null, IsDeleted = false, DeletedBy = null, DeletedDate = null, DeleteReason = null },
               new ApplicationRole { Id = AppIdentityDbEnums.ApplicationRoleEnum.Admin.ToDescriptionAttr(), Name = "Admin", NormalizedName = "Admin", IsActive = true, CreatedBy = AppIdentityDbEnums.ApplicationUserEnum.SuperAdmin.ToString(), CreatedDate = DateTime.UtcNow, UpdatedBy = null, UpdatedDate = null, IsDeleted = false, DeletedBy = null, DeletedDate = null, DeleteReason = null },
                new ApplicationRole { Id = AppIdentityDbEnums.ApplicationRoleEnum.User.ToDescriptionAttr(), Name = "User", NormalizedName = "User", IsActive = true, CreatedBy = AppIdentityDbEnums.ApplicationUserEnum.SuperAdmin.ToString(), CreatedDate = DateTime.UtcNow, UpdatedBy = null, UpdatedDate = null, IsDeleted = false, DeletedBy = null, DeletedDate = null, DeleteReason = null }
            );

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser { Id = AppIdentityDbEnums.ApplicationUserEnum.SuperAdmin.ToDescriptionAttr(), AccessFailedCount = 0, UserName = "superadmin@mail.com", NormalizedUserName = "superadmin@mail.com", Email = "superadmin@mail.com", NormalizedEmail = "superadmin@mail.com", EmailConfirmed = true, PasswordHash = "$2y$10$cSbpqoXGyPkCBPulaZ4MPu5OTuyU0RAULrXjWMcohImmNwqA2/7i6", SecurityStamp = "2828785b-8781-4423-af23-1578c0cfdf8e", ConcurrencyStamp = "8b9e0d94-c768-4d8b-a63b-5f2ccd32ab6e", IsActive = true, CreatedBy = AppIdentityDbEnums.ApplicationUserEnum.SuperAdmin.ToString(), CreatedDate = DateTime.UtcNow, UpdatedBy = null, UpdatedDate = null, IsDeleted = false, DeletedBy = null, DeletedDate = null, DeleteReason = null },
                new ApplicationUser { Id = AppIdentityDbEnums.ApplicationUserEnum.Admin.ToDescriptionAttr(), AccessFailedCount = 0, UserName = "admin@mail.com", NormalizedUserName = "admin@mail.com", Email = "superadmin@mail.com", NormalizedEmail = "admin@mail.com", EmailConfirmed = true, PasswordHash = "$2y$10$cSbpqoXGyPkCBPulaZ4MPu5OTuyU0RAULrXjWMcohImmNwqA2/7i6", SecurityStamp = "f9456c0d-2889-4ffe-a881-5977fac02fab", ConcurrencyStamp = "a441914d-4778-4468-9e7d-8975fb744c00", IsActive = true, CreatedBy = AppIdentityDbEnums.ApplicationUserEnum.SuperAdmin.ToString(), CreatedDate = DateTime.UtcNow, UpdatedBy = null, UpdatedDate = null, IsDeleted = false, DeletedBy = null, DeletedDate = null, DeleteReason = null },
                new ApplicationUser { Id = AppIdentityDbEnums.ApplicationUserEnum.User.ToDescriptionAttr(), AccessFailedCount = 0, UserName = "user@mail.com", NormalizedUserName = "user@mail.com", Email = "user@mail.com", NormalizedEmail = "user@mail.com", EmailConfirmed = true, PasswordHash = "$2y$10$cSbpqoXGyPkCBPulaZ4MPu5OTuyU0RAULrXjWMcohImmNwqA2/7i6", SecurityStamp = "3f507ad4-464c-47c4-8079-491d685f7f29", ConcurrencyStamp = "478b3ffd-c628-4c4f-8a8e-4b7d69193864", IsActive = true, CreatedBy = AppIdentityDbEnums.ApplicationUserEnum.SuperAdmin.ToString(), CreatedDate = DateTime.UtcNow, UpdatedBy = null, UpdatedDate = null, IsDeleted = false, DeletedBy = null, DeletedDate = null, DeleteReason = null }
            );

            builder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole { RoleId = AppIdentityDbEnums.ApplicationRoleEnum.SuperAdmin.ToDescriptionAttr(), UserId = AppIdentityDbEnums.ApplicationUserEnum.SuperAdmin.ToDescriptionAttr() },
                new ApplicationUserRole { RoleId = AppIdentityDbEnums.ApplicationRoleEnum.SuperAdmin.ToDescriptionAttr(), UserId = AppIdentityDbEnums.ApplicationUserEnum.Admin.ToDescriptionAttr() },
                new ApplicationUserRole { RoleId = AppIdentityDbEnums.ApplicationRoleEnum.SuperAdmin.ToDescriptionAttr(), UserId = AppIdentityDbEnums.ApplicationUserEnum.User.ToDescriptionAttr() },

                new ApplicationUserRole { RoleId = AppIdentityDbEnums.ApplicationRoleEnum.Admin.ToDescriptionAttr(), UserId = AppIdentityDbEnums.ApplicationUserEnum.Admin.ToDescriptionAttr() },
                new ApplicationUserRole { RoleId = AppIdentityDbEnums.ApplicationRoleEnum.Admin.ToDescriptionAttr(), UserId = AppIdentityDbEnums.ApplicationUserEnum.User.ToDescriptionAttr() },

                new ApplicationUserRole { RoleId = AppIdentityDbEnums.ApplicationRoleEnum.User.ToDescriptionAttr(), UserId = AppIdentityDbEnums.ApplicationUserEnum.User.ToDescriptionAttr() }
            );

           
        }
    }
}
