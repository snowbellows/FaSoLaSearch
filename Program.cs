using System.Globalization;
using System.Text;
using CsvHelper;
using FaSoLaSearch.Data;
using FaSoLaSearch.Models;
using Microsoft.AspNetCore.Mvc;
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
        return Results.Created($"/part/{part.PartId}", part);
    }
);

app.MapPost(
    "/parts/csv",
    async (
        PartContext db,
        [FromHeader(Name = "Content-Type")] string contentType,
        [FromBody] Stream body
    ) =>
    {
        try
        {
            if (!contentType?.StartsWith("text/csv") ?? true)
            {
                return Results.BadRequest("Content-Type must be text/csv");
            }

            using var reader = new StreamReader(body);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            // Configure CSV mapping
            csv.Context.RegisterClassMap<PartMap>();

            var parts = csv.GetRecordsAsync<Part>().ToBlockingEnumerable().ToList();

            // Validate records
            if (parts.Count == 0)
            {
                return Results.BadRequest("No valid records found in CSV");
            }

            await db.Parts.AddRangeAsync(parts);
            await db.SaveChangesAsync();

            return Results.Created("/parts", new { Count = parts.Count });
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Error processing CSV: {ex.Message}");
        }
    }
);

app.Run();
