using Microsoft.AspNetCore.Identity;
using TODOapp.Models;

namespace TODOapp.Data;

public class ApplicationDbContextSeed {
    public static async Task SeedEssentialsAsync(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager) {
        await roleManager.CreateAsync(new IdentityRole<Guid> { Name = "Admin", NormalizedName = "ADMIN" });
        await roleManager.CreateAsync(new IdentityRole<Guid> { Name = "User", NormalizedName = "USER" });
        
        var admin = new User { UserName = "admin", Email = "admin" };

        if (userManager.Users.All(u => u.Id != admin.Id))
        {
            await userManager.CreateAsync(admin, "Admin123!");
            await userManager.AddToRoleAsync(admin, "ADMIN");
        }
    }
}