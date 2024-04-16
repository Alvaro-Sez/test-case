using Microsoft.AspNetCore.Identity;

namespace ClayLocks.IDP;

public static class IdpConfiguration
{
    public static void AddIdpProvider(this IServiceCollection services)
    {
        services 
            .AddAuthorization()
            .AddIdentityApiEndpoints<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityData>();
    }
}