using System.Diagnostics;
using ClayLocks.IDP;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ClayLocks.Configuration;

public static class SeedIdp
{
    public static void AddIdpData(this WebApplication app)
    {
        // try
        // {
        //     using var sp = app.Services.CreateScope();
        //      var userManager = sp.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        //      var roleManager = sp.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //     UserAndRoleDataInitializer.SeedData(userManager, roleManager);
        // }
        // catch(Exception ex)
        // {
        //     Debug.WriteLine(ex.Message);
        // }
    }
}