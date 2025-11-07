using FaSoLaSearch.Data;
using FaSoLaSearch.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add PostgreSQL database context
builder.Services.AddDbContext<PartContext>(options =>
    options.UseNpgsql(builder.Configuration["Parts:DatabaseConnection"])
);

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

app.MapGet("/part", async (PartContext db) => await db.Parts.ToListAsync());

app.MapPost(
    "/part",
    async (PartContext db, Part part) =>
    {
        await db.Parts.AddAsync(part);
        await db.SaveChangesAsync();
        return Results.Created($"/pizza/{part.PartId}", part);
    }
);

app.Run();
