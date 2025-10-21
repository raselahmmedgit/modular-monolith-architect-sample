using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rapid.erp.Common;
using rapid.erp.Core.Extensions;
using rapid.erp.Core.FlashMessage;
using rapid.erp.Core.Mapping;
using rapid.erp.Core.Security;
using rapid.erp.Core.Service;
using rapid.erp.Core.Utility;
using rapid.erp.EntityModel;

namespace rapid.erp.Core.Dependency
{
    /// <summary>
    /// Dependency Configuration middleware for SPA.
    /// </summary>
    public static class DependencyConfiguration
    {
        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = AppConstants.DefaultConnectionString;

            if (AppConstants.IsMSSqlDatabase == true)
            {
                //MS SqlServer
                services.AddDbContext<AppIdentityDbContext>(options =>
                    options.UseSqlServer(connectionString).EnableDetailedErrors());

                services.AddDbContext<AppDbContext>(options =>
                  options.UseSqlServer(connectionString));
            }
            else if (AppConstants.IsMySqlDatabase == true)
            {
                //MySQL
                services.AddDbContext<AppIdentityDbContext>(options =>
                    options.UseMySQL(connectionString).EnableDetailedErrors());

                services.AddDbContext<AppDbContext>(options =>
                  options.UseMySQL(connectionString));
            }
            else if (AppConstants.IsPostgreSQLDatabase == true)
            {
                //PostgreSQL
                services.AddDbContext<AppIdentityDbContext>(options =>
                    options.UseNpgsql(connectionString).EnableDetailedErrors());

                services.AddDbContext<AppDbContext>(options =>
                  options.UseNpgsql(connectionString));
            }
            else if (AppConstants.IsOracleDatabase == true)
            {
                //Oracle
                services.AddDbContext<AppIdentityDbContext>(options =>
                    options.UseNpgsql(connectionString).EnableDetailedErrors());

                services.AddDbContext<AppDbContext>(options =>
                  options.UseNpgsql(connectionString));
            }

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();

            //services.AddDatabaseDeveloperPageExceptionFilter();

            //AppDbContextStartup.Run(configuration);
            AppIdentityDbContextStartup.Run(configuration);

        }

        public static void RegisterRepositoryAndManager(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services.RegisterCustomRoute();

            services.AddCors();

            #region MemoryCache
            //services.AddDistributedMemoryCache();
            //services.AddMemoryCache();
            #endregion

            //services.AddMvc(
            //   options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
            //);
            //call this in case you need aspnet-user-authtype/aspnet-user-identity
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();

            services.RegisterAutoMapper();

            services.RegisterAllTypes(typeof(rapid.erp.IManager.IDependencyManager).Assembly);
            services.RegisterAllTypes(typeof(rapid.erp.IRepository.IDependencyRepository).Assembly);
            services.RegisterAllTypes(typeof(rapid.erp.Manager.DependencyManager).Assembly);
            services.RegisterAllTypes(typeof(rapid.erp.Repository.DependencyRepository).Assembly);

            //services.AddTransient<RoleManager<ApplicationRole>>();
            //services.AddTransient<UserManager<ApplicationUser>>();
            //services.AddTransient<IApplicationRoleManager, ApplicationRoleManager>();
            //services.AddTransient<IApplicationUserManager, ApplicationUserManager>();
            //services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();
            //services.AddScoped<IApplicationUserManager, ApplicationUserManager>();

            services.AddSingleton<SearchModel>();
            services.AddTransient<IViewRenderService, ViewRenderService>();
            
            services.RegisterSecurityServices(configuration);
            
            services.AddFlashMessage();

        }

    }
}
