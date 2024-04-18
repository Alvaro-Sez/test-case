using EventBus;
using Read.Data.DI;
using Read.Implementation.DI;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.AddReadServices();
builder.Services.AddReadDataServices();

builder.Services.AddCap(options =>
{
    options.UseInMemoryStorage();
    options.UseRabbitMQ(Environment.GetEnvironmentVariable("ENV_RABBIT")!);
});

var host = builder.Build();
host.Run();