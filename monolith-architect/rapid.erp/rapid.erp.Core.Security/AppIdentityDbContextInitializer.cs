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
                    var isCreated = context.Database.EnsureCreated();

                    if (isCreated)
                    {
                        AppConstants.IsDatabaseCreated = true;

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
