using Microsoft.EntityFrameworkCore;
using rapid.erp.Core.Utility;

namespace rapid.erp.EntityModel
{
    public static class AppDbContextInitializer
    {
        public static bool CreateIfNotExists()
        {
            using (var context = new AppDbContext())
            {
                var canConnect = context.Database.CanConnect();
                if (canConnect)
                {
                    var isCreated = context.Database.EnsureCreated();

                    if (isCreated)
                    {
                        AppConstants.IsDatabaseCreated = true;

                        //if (AppConstants.IsMasterDataInserted == false)
                        //{
                        //    //ModelBuilder modelBuilder = new ModelBuilder();
                        //    //AppDbContextSeedData.SeedData(modelBuilder);
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
