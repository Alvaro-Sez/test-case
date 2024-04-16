using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClayLocks.IDP;

public class IdentityData :IdentityDbContext
{
    public IdentityData (DbContextOptions<IdentityData> options): base(options){}
}