using ClayLocks.Configuration;
using Read.Data.DI;
using Read.Implementation.DI;
using Write.Implementation.DI;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.AddClientCert();//could not implement in in docker

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddReadServices();
builder.Services.AddReadDataServices();
builder.Services.AddWriteServices();
builder.Services.AddControllers();

builder.Services.AddCap(options =>
{
    options.UseInMemoryStorage();
    options.UseRabbitMQ("localhost");
    options.ConsumerThreadCount = 0;
    options.UseDashboard();
});
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
