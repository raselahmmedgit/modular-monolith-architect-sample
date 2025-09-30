using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using rapid.erp.Core.Dependency;
using rapid.erp.Core.Helpers;
using rapid.erp.Core.Security;
using rapid.erp.Core.Utility;
using rapid.erp.EntityModel;
using rapid.erp.ViewModel.Config;
using System.Configuration;
using System.Threading.Tasks;

namespace rapid.erp.WebMvc
{
    public class BootStrapper
    {
        public static void Run(IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                // Add functionality to inject IOptions<T>
                services.AddOptions();

                //Add our Config object so it can be injected
                services.Configure<ConnectionStrings>(configuration.GetSection(ConnectionStrings.Name));
                services.Configure<SmsConfig>(configuration.GetSection(SmsConfig.Name));
                services.Configure<EmailConfig>(configuration.GetSection(EmailConfig.Name));
                services.Configure<AppConfig>(configuration.GetSection(AppConfig.Name));

                //Set AppConstants Value
                AppConstants.DefaultConnectionString = configuration.GetConnectionString("DefaultConnection");
                AppConstants.IsMSSqlDatabase = configuration["AppConfig:IsMSSqlDatabase"] == null ? true : bool.Parse(configuration["AppConfig:IsMSSqlDatabase"].ToString());
                AppConstants.IsMySqlDatabase = configuration["AppConfig:IsMySqlDatabase"] == null ? true : bool.Parse(configuration["AppConfig:IsMySqlDatabase"].ToString());
                AppConstants.IsPostgreSQLDatabase = configuration["AppConfig:IsPostgreSQLDatabase"] == null ? true : bool.Parse(configuration["AppConfig:IsPostgreSQLDatabase"].ToString());
                AppConstants.IsOracleDatabase = configuration["AppConfig:IsOracleDatabase"] == null ? true : bool.Parse(configuration["AppConfig:IsOracleDatabase"].ToString());
                AppConstants.IsDatabaseCreated = configuration["AppConfig:IsDatabaseCreated"] == null ? true : bool.Parse(configuration["AppConfig:IsDatabaseCreated"].ToString());
                AppConstants.IsMasterDataInserted = configuration["AppConfig:IsMasterDataInserted"] == null ? true : bool.Parse(configuration["AppConfig:IsMasterDataInserted"].ToString());
                //Set AppConstants Value

                //Register config
                AppConfigHelper.Init(configuration);

                DependencyConfiguration.RegisterDbContext(services, configuration);

                services.Configure<IdentityOptions>(options =>
                {
                    // Password settings.
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings.
                    options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = false;
                });

                services.ConfigureApplicationCookie(options =>
                {
                    // Cookie settings
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromDays(30);
                    options.LogoutPath = "/Account/Logout";
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.SlidingExpiration = true;
                });

                DependencyConfiguration.RegisterRepositoryAndManager(services, configuration);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static async Task RunDbContextSeedData(IServiceProvider servicesProvider) 
        {
            try
            {
                await AppIdentityDbContextSeedData.SeedDataAsync(servicesProvider);
                await AppDbContextSeedData.SeedDataAsync(servicesProvider);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
    }
}
