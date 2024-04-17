using System.Diagnostics;
using ClayLocks.IDP;
using Microsoft.AspNetCore.Identity;

namespace ClayLocks.Configuration;

public static class SeedIdp
{
    public static void AddIdpData(this WebApplication app)
    {
        try
        {
            var userManager = app.Services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = app.Services.GetRequiredService<RoleManager<IdentityRole>>();
            UserAndRoleDataInitializer.SeedData(userManager, roleManager);
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}