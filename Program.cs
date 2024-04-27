using UserStore.Models;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DB Configurations
var connectionString = builder.Configuration.GetConnectionString("Users") ?? "Data Source=Users.db";
builder.Services.AddSqlite<UserDb>(connectionString);

// Swagger Configurations
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config => {
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Users API",
        Description = "User Management Service",
        Version = "v1"
    });
});

// Build App
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.SwaggerEndpoint("/swagger/v1/swagger.json", "Users API V1");
});


// Users Endpoints and methods.
const string UsersEnpoint = "/users";
// GET: /users
app.MapGet(UsersEnpoint, async (UserDb db) => await db.Users.ToListAsync());

// GET: /users/{id}
app.MapGet(UsersEnpoint + "/{id}", async (UserDb db, int id) => await db.Users.FindAsync(id));

// POST: /users
app.MapPost(UsersEnpoint, async (UserDb db, User user) =>
{
    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return Results.Created($"/user/{user.Id}", user);
});

// PUT: /users/{id}
app.MapPut(UsersEnpoint + "/{id}", async (UserDb db, User updateuser, int id) =>
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
app.MapDelete(UsersEnpoint + "/{id}", async (UserDb db, int id) => {
    var user = await db.Users.FindAsync(id);
    if (user is null) {
        return Results.NotFound();
    }
    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/", () => "Backend Developer Technical Test!");

// Start App
app.Run();
