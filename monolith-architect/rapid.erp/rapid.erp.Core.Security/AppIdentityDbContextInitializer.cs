using Microsoft.EntityFrameworkCore;
using rapid.erp.Core.Utility;

namespace rapid.erp.Core.Security
{
    public static class AppIdentityDbContextInitializer
    {
        public static bool CreateIfNotExists()
        {
            using (var context = new AppIdentityDbContext())
            {
                var canConnect = context.Database.CanConnect();
                if (canConnect)
                {
                    //context.Database.Migrate();

                    if (AppConstants.IsDatabaseCreated)
                    {
                        //if (AppConstants.IsMasterDataInserted == false)
                        //{
                        //    ModelBuilder modelBuilder = new ModelBuilder();
                        //    AppIdentityDbContextSeedData.SeedData(modelBuilder);
                        //}

                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
