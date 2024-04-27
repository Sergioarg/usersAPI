using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserStore.Models;

var builder = WebApplication.CreateBuilder(args);
// Config Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<UserDb>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddSwaggerGen(config => {
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Users API",
        Description = "Get and manage users data",
        Version = "v1"
    });
});

var app = builder.Build();


// Using Swagger
string swaggerPath = "/swagger/v1/swagger.json";

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.SwaggerEndpoint(swaggerPath, "Users API V1");
});

// GET: /users
app.MapGet("/users", async (UserDb db) => await db.Users.ToListAsync());

// GET: /users/{id}
app.MapGet("/users/{id}", async (UserDb db, int id) => await db.Users.FindAsync(id));

// POST: /users
app.MapPost("/users", async (UserDb db, User user) =>
{
    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return Results.Created($"/user/{user.Id}", user);
});

// PUT: /users/{id}
app.MapPut("/users/{id}", async (UserDb db, User updateuser, int id) =>
{
    var user = await db.Users.FindAsync(id);
    if (user is null) return Results.NotFound();
    user.Name = updateuser.Name;
    user.Birthdate = updateuser.Birthdate;
    user.Active = updateuser.Active;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// DELETE: /users/{id}
app.MapDelete("/users/{id}", async (UserDb db, int id) => {
    var user = await db.Users.FindAsync(id);
    if (user is null) {
        return Results.NotFound();
    }
    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/", () => "I'm alive!");

app.Run();
