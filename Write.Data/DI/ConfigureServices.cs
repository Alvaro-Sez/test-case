using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Write.Contacts.Repository;
using Write.Data.EF;
using Write.Data.Repository;

namespace Write.Data.DI;

public static class ConfigureServices
{
    public static void AddWriteDataServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<ApplicationDbContext>(opt=>
            opt.UseSqlServer(configuration.GetConnectionString("ENV_SQL_URL"))
        );
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped<IIqRepository,IqRepository>();
    }
}
