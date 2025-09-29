using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace rapid.erp.Core.Security
{
    public static class AppIdentityDbContextSeedData
    {
        public static async Task SeedDataAsync(IServiceProvider servicesProvider)
        {
            using (var scope = servicesProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var context = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

                // Apply Migrate
                await context.Database.MigrateAsync();

                // Seed Data - Roles
                var roles = new[] { "Admin", "Manager", "User" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // Seed Data - Admin User
                var adminEmail = "admin@mail.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new IdentityUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(adminUser, "Admin123456"); // Strong password required
                    if (result.Succeeded)
                    {
                        // Seed Data - Assign Admin Role
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }

                // Seed Data - Manager User
                var managerEmail = "manager@mail.com";
                var managerUser = await userManager.FindByEmailAsync(managerEmail);
                if (managerUser == null)
                {
                    managerUser = new IdentityUser
                    {
                        UserName = managerEmail,
                        Email = managerEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(managerUser, "Manager123456"); // Strong password required
                    if (result.Succeeded)
                    {
                        // Seed Data - Assign Manager Role
                        await userManager.AddToRoleAsync(managerUser, "Manager");
                    }
                }


                // Seed Data - Employee User
                var employeeEmail = "employee@mail.com";
                var employeeUser = await userManager.FindByEmailAsync(employeeEmail);
                if (employeeUser == null)
                {
                    employeeUser = new IdentityUser
                    {
                        UserName = employeeEmail,
                        Email = employeeEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(employeeUser, "Employee123456"); // Strong password required
                    if (result.Succeeded)
                    {
                        // Seed Data - Assign Employee Role
                        await userManager.AddToRoleAsync(employeeUser, "User");
                    }
                }

            }
           
        }
    }
}
