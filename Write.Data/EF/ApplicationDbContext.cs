using Microsoft.EntityFrameworkCore;
using Write.Contacts.Entities;
using Write.Data.EF.EntityConfiguration;

namespace Write.Data.EF;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }
    public DbSet<Iq>  Iqs => Set<Iq>();
    public DbSet<User> Users => Set<User>();
    public DbSet<BindIqRequest>  BindRequests => Set<BindIqRequest>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new IqConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new BindRequestConfiguration());
    }
    
}