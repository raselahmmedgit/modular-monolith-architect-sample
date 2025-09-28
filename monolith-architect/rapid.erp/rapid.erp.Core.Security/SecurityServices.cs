using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rapid.erp.Core.Security.JwtGenerator;

namespace rapid.erp.Core.Security
{
    /// <summary>
    /// Dependency Injection extention for Security Services.
    /// </summary>
    public static class SecurityServices
    {
        public static void RegisterSecurityServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient<IAppComponentRepository, AppComponentRepository>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
        }

        public static void RegisterSecurityServicesAPI(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient<IAppComponentRepository, AppComponentRepository>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
        }
    }
}
