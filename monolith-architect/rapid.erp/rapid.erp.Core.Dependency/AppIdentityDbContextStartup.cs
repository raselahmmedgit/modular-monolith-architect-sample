using Microsoft.Extensions.Configuration;
using rapid.erp.Core.Helpers;
using rapid.erp.Core.Security;
using rapid.erp.Core.Utility;

namespace rapid.erp.Core.Dependency
{
    public static class AppIdentityDbContextStartup
    {
        public static void Run(IConfiguration configuration)
        {
            try
            {
                if (AppConstants.IsDatabaseCreated == false)
                {
                    var isAppDatabaseCreated = AppIdentityDbContextInitializer.CreateIfNotExists();

                    if (isAppDatabaseCreated)
                    {
                        // Set value in memory
                        AppConfigHelper.Set("AppConfig:IsDatabaseCreated", "true");

                        // Persist to file
                        AppConfigHelper.Persist("AppConfig:IsDatabaseCreated", "true");
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
