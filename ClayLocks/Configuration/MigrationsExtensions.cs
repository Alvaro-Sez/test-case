using Microsoft.EntityFrameworkCore;
using Write.Data.EF;

namespace ClayLocks.Configuration;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()!;
        dbContext.Database.Migrate();
    }
}
