using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

const string? connectionString = "Server=(local); Database=Blog; Integrated Security=SSPI; TrustServerCertificate=True; App=BlogApi;";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbLogic>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/posts", async (DbLogic db) =>
    await db.Posts.ToListAsync());

app.MapGet("/posts/{id}", async (int id, DbLogic db) =>
    await db.Posts.FindAsync(id)
        is Post posts
            ? Results.Ok(posts)
            : Results.NotFound());


// more endpoints here

app.Run();
