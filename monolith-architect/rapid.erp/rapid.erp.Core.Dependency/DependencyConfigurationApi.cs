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
using rapid.erp.EntityModel;

namespace rapid.erp.Core.Dependency
{
    /// <summary>
    /// Dependency Configuration middleware for API.
    /// </summary>
    public static class DependencyConfigurationApi
    {
        public static void RegisterDbAndMapper(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("dev");

            //services.AddDbContext<AppIdentityDbContext>(options =>
            //    options.UseSqlServer(
            //        configuration.GetConnectionString("dev")));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<AppIdentityDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddDbContext<AppDbContext>(options =>
              options.UseSqlServer(
                  configuration.GetConnectionString("dev")).EnableDetailedErrors()
              );

            // services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.RegisterCustomRoute();

            // Add functionality to inject IOptions<T>
            services.AddOptions();

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

            //Settings

            services.AddSingleton<SearchModel>();
            services.AddTransient<IViewRenderService, ViewRenderService>();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.RegisterSecurityServices(configuration);
            services.AddFlashMessage();

        }
    }
}
