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

// GET all parts with optional note filters
app.MapGet(
    "/part",
    async (
        PartContext db,
        [FromQuery] string? partName,
        [FromQuery] char? first,
        [FromQuery] char? second,
        [FromQuery] char? third,
        [FromQuery] char? fourth,
        [FromQuery] char? fifth,
        [FromQuery] char? sixth,
        [FromQuery] char? seventh,
        [FromQuery] char? eighth
    ) =>
    {
        var query = db.Parts.AsQueryable();

        if (partName is not null)
            query = query.Where(p => p.Name.ToLower() == partName.ToLower());
        if (first.HasValue)
            query = query.Where(p => p.First == first.Value);
        if (second.HasValue)
            query = query.Where(p => p.Second == second.Value);
        if (third.HasValue)
            query = query.Where(p => p.Third == third.Value);
        if (fourth.HasValue)
            query = query.Where(p => p.Fourth == fourth.Value);
        if (fifth.HasValue)
            query = query.Where(p => p.Fifth == fifth.Value);
        if (sixth.HasValue)
            query = query.Where(p => p.Sixth == sixth.Value);
        if (seventh.HasValue)
            query = query.Where(p => p.Seventh == seventh.Value);
        if (eighth.HasValue)
            query = query.Where(p => p.Eighth == eighth.Value);

        return await query.ToListAsync();
    }
);

// GET part by id
app.MapGet(
    "/part/{id}",
    async (PartContext db, int id) =>
    {
        var part = await db.Parts.FindAsync(id);
        return part is null ? Results.NotFound() : Results.Ok(part);
    }
);

// POST new part
app.MapPost(
    "/part",
    async (PartContext db, Part part) =>
    {
        await db.Parts.AddAsync(part);
        await db.SaveChangesAsync();
        return Results.Created($"/part/{part.PartId}", part);
    }
);

// PUT update part
app.MapPut(
    "/part/{id}",
    async (PartContext db, int id, Part updatedPart) =>
    {
        if (id != updatedPart.PartId)
        {
            return Results.BadRequest("ID mismatch");
        }

        var part = await db.Parts.FindAsync(id);
        if (part is null)
        {
            return Results.NotFound();
        }

        // Update properties
        part.SongNumber = updatedPart.SongNumber;
        part.SongName = updatedPart.SongName;
        part.Name = updatedPart.Name;
        part.First = updatedPart.First;
        part.Second = updatedPart.Second;
        part.Third = updatedPart.Third;
        part.Fourth = updatedPart.Fourth;
        part.Fifth = updatedPart.Fifth;
        part.Sixth = updatedPart.Sixth;
        part.Seventh = updatedPart.Seventh;
        part.Eighth = updatedPart.Eighth;

        await db.SaveChangesAsync();
        return Results.NoContent();
    }
);

// DELETE part
app.MapDelete(
    "/part/{id}",
    async (PartContext db, int id) =>
    {
        var part = await db.Parts.FindAsync(id);
        if (part is null)
        {
            return Results.NotFound();
        }

        db.Parts.Remove(part);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
);

// POST bulk new parts from CSV
app.MapPost(
    "/part/csv",
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
