using rapid.erp.EntityModel.Admin;

namespace rapid.erp.EntityModel
{
    public static class AppDbContextSeedData
    {
        public static void SeedData(AppDbContext context)
        {
            if (!context.AppSetting.Any())
            {
                context.AppSetting.Add(new AppSetting { AppSettingId = 1, AppSettingName = "Application Name", Key = "ApplicationName", Value = "Rapid ERP" });
                context.SaveChanges();
            }
        }
    }
}
