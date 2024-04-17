using Microsoft.AspNetCore.Identity;

namespace ClayLocks.IDP;

public static class UserAndRoleDataInitializer
{
    public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        SeedRoles(roleManager);
        SeedUsers(userManager);
    }
 
    private static void SeedUsers (UserManager<IdentityUser> userManager)
    {
        if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
        {
            IdentityUser user = new IdentityUser();
            user.UserName = "admin";
            user.Email = "admin@gmail.com";
 
            IdentityResult result = userManager.CreateAsync(user, "123456Aa?").Result;
 
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
 
    private static void SeedRoles (RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Admin";
            IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
        }
    }
}