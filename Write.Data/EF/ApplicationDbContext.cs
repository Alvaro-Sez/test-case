using Microsoft.EntityFrameworkCore;
using Write.Contacts.Entities;
using Write.Data.EF.EntityConfiguration;

namespace Write.Data.EF;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }
    public DbSet<Iq>  Iqs => Set<Iq>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Lock> Locks => Set<Lock>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new IqConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new LockConfiguration());
    }
    
}