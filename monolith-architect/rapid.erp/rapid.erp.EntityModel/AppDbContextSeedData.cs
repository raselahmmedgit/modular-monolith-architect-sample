using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using rapid.erp.EntityModel.Admin;

namespace rapid.erp.EntityModel
{
    public static class AppDbContextSeedData
    {
        public static async Task SeedDataAsync(IServiceProvider servicesProvider)
        {
            using (var scope = servicesProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Apply Migrate
                await context.Database.MigrateAsync();

                if (!context.AppSetting.Any())
                {
                    await context.AppSetting.AddAsync(new AppSetting { AppSettingName = "Application Name", Key = "ApplicationName", Value = "Rapid ERP" });
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
