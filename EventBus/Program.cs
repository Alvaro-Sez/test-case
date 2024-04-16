using EventBus;
using Read.Implementation.Module;
using Write.Implementation.Module;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.AddWriteServices();
builder.Services.AddReadServices();

builder.Services.AddCap(options =>
{
    options.UseInMemoryStorage();
    options.UseRabbitMQ("localhost");
});

var host = builder.Build();
host.Run();