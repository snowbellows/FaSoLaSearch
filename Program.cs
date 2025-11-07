using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger services in development environment
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc(
            "v1",
            new OpenApiInfo
            {
                Title = "FaSoLaSearch API",
                Version = "v1",
                Description = "API for searching Sacred Harp songs",
            }
        );
    });
}

var app = builder.Build();

// Configure Swagger middleware in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FaSoLaSearch API V1");
    });
}

app.MapGet("/", () => "Hello World!");

app.Run();
