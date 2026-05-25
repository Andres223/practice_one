using Scalar.AspNetCore;
using WebApplication1.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Agregando repositories
builder.Services.AddRepositories();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Database Config
builder.Services.AddDatabaseConfig(builder.Configuration);

// Mapster config
builder.Services.AddMapsterConfig();

// OpenApi - Swagger generator - para que funcione Scalar
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecksConfig(builder.Configuration); // Agregar HealthCheck

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecksConfig(); // Endpoint probar HealthCheck

app.Run();