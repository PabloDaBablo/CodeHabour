using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace WMBA_7_2_.Data
{
    public static class ApplicationDbInitializer
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                try
                {
                    context.Database.Migrate();

                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    string[] roleNames = { "Admin", "Convenor", "Coaches", "Scorekeeper" }; 
                    IdentityResult roleResult;
                    foreach (var roleName in roleNames)
                    {
                        var roleExist = await roleManager.RoleExistsAsync(roleName);
                        if (!roleExist)
                        {
                            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                        }
                    }

                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    await CreateUserAsync(userManager, "admin@outlook.com", "Admin");
                    await CreateUserAsync(userManager, "convenor@outlook.com", "Convenor");
                    await CreateUserAsync(userManager, "coaches@outlook.com", "Coaches");
                    await CreateUserAsync(userManager, "scorekeeper@outlook.com", "Scorekeeper");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.GetBaseException().Message);
                }
            }
        }

        private static async Task CreateUserAsync(UserManager<IdentityUser> userManager, string email, string role)
        {
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                IdentityResult result = await userManager.CreateAsync(user, "Pa55w@rd");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    Debug.WriteLine($"Failed to create user {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}