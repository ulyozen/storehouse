using Warehouse360.Application.Compositions;
using Warehouse360.Persistence.Compositions;
using Warehouse360.Persistence.Configurations;
using Warehouse360.Redis.Compositions;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplicationExtension()
    .AddJwtAuthenticationExtensions(config)
    .AddPersistenceExtension(config)
    .AddRedisExtension(config);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseJwtAuthenticationAndAuthorization();

app.MapControllers();

var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();