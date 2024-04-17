using System.Diagnostics;
using ClayLocks.Configuration;
using ClayLocks.IDP;
using Microsoft.AspNetCore.Identity;
using Read.Data.DI;
using Read.Implementation.DI;
using Write.Data.DI;
using Write.Implementation.DI;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.AddClientCert();//could not implement in in docker

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddReadServices();
builder.Services.AddReadDataServices();
builder.Services.AddWriteServices();
builder.Services.AddIdpProvider();
builder.Services.AddWriteDataServices(builder.Configuration);
builder.Services.AddCap(options =>
{
    options.UseInMemoryStorage();
    options.UseRabbitMQ(Environment.GetEnvironmentVariable("ENV_RABBIT")!);
    options.ConsumerThreadCount = 0;
    options.UseDashboard();
});
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.AddIdpData();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
