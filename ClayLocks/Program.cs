using Read.Implementation.Module;
using Write.Implementation.Module;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddReadServices();
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
