using System.Net;
using Microsoft.AspNetCore.Identity;
using TODOapp.api.Settings;
using TODOapp.Models;

namespace TODOapp.Data;

public class ApplicationDbContextSeed
{
    public static async Task SeedEssentialsAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Roles
        await roleManager.CreateAsync(new IdentityRole(Roles.Administrator.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
        
        var admin = new User { UserName = "admin", Email = "admin"};

        if (userManager.Users.All(u => u.Id != admin.Id))
        {
            await userManager.CreateAsync(admin, "admin");
            await userManager.AddToRoleAsync(admin, Roles.Administrator.ToString());
        }
    }
}