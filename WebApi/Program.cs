using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

const string? connectionString = "Server=(local); Database=Blog; Integrated Security=SSPI; TrustServerCertificate=True; App=BlogApi;";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbLogic>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();


// GETS
app.MapGet("/posts", async (DbLogic db) =>
    await db.Posts.ToListAsync());

app.MapGet("/posts/{id}", async (int id, DbLogic db) =>
    await db.Posts.FindAsync(id)
        is Post posts
            ? Results.Ok(posts)
            : Results.NotFound());

app.MapGet("/categories", async (DbLogic db) =>
    await db.Categories.ToListAsync());


// POSTS
app.MapPost("/posts", async (Post input, DbLogic db) =>
{ 
    if (input.Title is null || input.Contents is null) return Results.BadRequest();

    if (await db.Categories.FindAsync(input.Category) is not Category category) return Results.BadRequest();

    Post post = new()
    {
        Title = input.Title,
        Contents = input.Contents,
        Category = input.Category,
        Timestamp = DateTime.UtcNow,
    };
    db.Posts.Add(post);
    await db.SaveChangesAsync();

    return Results.Created($"/posts/{post.Id}", post);
});


// PUTS
app.MapPut("/posts/{id}", async (int id, Post input, DbLogic db) =>
{
    if (input.Title is null || input.Contents is null) return Results.BadRequest();

    var existing = await db.Posts.FindAsync(id);
    if (existing is null) return Results.NotFound();

    existing.Title = input.Title;
    existing.Contents = input.Contents;
    existing.Category = input.Category;

    await db.SaveChangesAsync();
    return Results.Accepted($"/posts/{existing.Id}", existing);
});


// DELETES
app.MapDelete("/posts", async (DbLogic db) =>
{
    await db.Posts.ExecuteDeleteAsync();
    return Results.NoContent();
});

app.MapDelete("/posts/{id}", async (int id, DbLogic db) =>
{
    if (await db.Posts.FindAsync(id) is Post post)
    {
        db.Posts.Remove(post);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});


app.Run();
