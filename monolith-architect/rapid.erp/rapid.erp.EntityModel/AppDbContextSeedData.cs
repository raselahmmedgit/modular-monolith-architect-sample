using Microsoft.EntityFrameworkCore;
using rapid.erp.EntityModel.Security;

namespace rapid.erp.EntityModel
{
    public static class AppDbContextSeedData
    {
        public static void SeedData(ModelBuilder builder)
        {
            builder.Entity<AppSetting>().HasData(
                new AppSetting { AppSettingId = 1, AppSettingName = "Application Name", Key = "ApplicationName", Value = "Rapid ERP" },
                new AppSetting { AppSettingId = 2, AppSettingName = "", Key = "", Value = "" },
                new AppSetting { AppSettingId = 3, AppSettingName = "", Key = "", Value = "" },
                new AppSetting { AppSettingId = 4, AppSettingName = "", Key = "", Value = "" },
                new AppSetting { AppSettingId = 5, AppSettingName = "", Key = "", Value = "" },
                new AppSetting { AppSettingId = 6, AppSettingName = "", Key = "", Value = "" },
                new AppSetting { AppSettingId = 7, AppSettingName = "", Key = "", Value = "" },
                new AppSetting { AppSettingId = 8, AppSettingName = "", Key = "", Value = "" },
                new AppSetting { AppSettingId = 9, AppSettingName = "", Key = "", Value = "" },
                new AppSetting { AppSettingId = 10, AppSettingName = "", Key = "", Value = "" }
            );

        }
    }
}
