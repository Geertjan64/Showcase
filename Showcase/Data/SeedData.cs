using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Showcase.Areas.Identity.Data;

public class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ShowcaseUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string adminEmail = "admin@admin.nl";
        string adminPassword = "Admin123!";

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new ShowcaseUser
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
