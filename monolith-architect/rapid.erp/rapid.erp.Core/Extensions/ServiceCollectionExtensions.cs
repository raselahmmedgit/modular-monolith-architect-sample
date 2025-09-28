using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace rapid.erp.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterAllTypes(this IServiceCollection services, Assembly assemblie, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var typesFromAssemblies = assemblie.DefinedTypes.Where(x => x.Name.Contains("Repository") || x.Name.Contains("Manager") || x.Name.Contains("IManager") || x.Name.Contains("IRepository"));

            foreach (var type in typesFromAssemblies)
            {
                var itypesFromAssemblies = type.Assembly.DefinedTypes.Where(x => x.GetTypeInfo() == type).SelectMany(c => c.GetInterfaces());
                foreach (var iface in itypesFromAssemblies)
                {

                    if (iface.GetMembers().Any())
                    {
                        services.Add(new ServiceDescriptor(iface, type, lifetime));
                    }
                }
            }
        }

        public static void RegisterCustomRoute(this IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationFormats.Add("/Views/{1}/Partials/{0}.cshtml");
                options.ViewLocationFormats.Add("/Views/{1}/Modals/{0}.cshtml");
                options.ViewLocationFormats.Add("/Views/Shared/Partials/{0}.cshtml");
                options.ViewLocationFormats.Add("/Views/Shared/Modals/{0}.cshtml");
                options.ViewLocationFormats.Add("/Views/Shared/Scripts/{0}.cshtml");
                options.ViewLocationFormats.Add("/Views/{2}/{1}/{0}.cshtml");
            });
        }
    }
}
