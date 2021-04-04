namespace Schuellerrat.Data.Seeders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (dbContext.Users.Any())
            {
                return;
            }

            await roleManager.CreateAsync(new IdentityRole("admin"));
            await userManager.CreateAsync(new IdentityUser("schuellerrat"), "obsuveta");
            var user = await userManager.FindByNameAsync("schuellerrat");
            await userManager.AddToRoleAsync(user, "admin");
        }
    }
}
