using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rapid.erp.Core.Helpers;
using rapid.erp.Core.Utility;
using rapid.erp.EntityModel;

namespace rapid.erp.Core.Dependency
{
    public static class AppDbContextStartup
    {
        public static void Run(IConfiguration configuration)
        {
            try
            {
                if (AppConstants.IsDatabaseCreated == false)
                {
                    var isAppDatabaseCreated = AppDbContextInitializer.CreateIfNotExists();

                    if (isAppDatabaseCreated)
                    {
                        //// Set value in memory
                        //AppConfigHelper.Set("AppConfig:IsDatabaseCreated", "true");

                        //// Persist to file
                        //AppConfigHelper.Persist("AppConfig:IsDatabaseCreated", "true");
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
