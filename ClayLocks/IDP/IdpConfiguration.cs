using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClayLocks.IDP;

public static class IdpConfiguration
{
    public static void AddIdpProvider(this IServiceCollection services)
    {
       services.AddDbContext<IdentityData>(opt =>
           opt.UseInMemoryDatabase("IdpDatabase"));
       
       services
            .AddAuthorization()
            .AddIdentityApiEndpoints<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityData>();
    }
}