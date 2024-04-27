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

// Users Endpoints and Methods.
var users = app.MapGroup("/users");
users.MapGet("/", GetAllUsers);      // GET:    /users
users.MapGet("/{id}", GetUser);      // GET:    /users/{id}
users.MapPost("/", CreateUser);      // POST:   /users
users.MapPut("/{id}", UpdateUser);   // PUT:    /users/{id}
users.MapDelete("/{id}", DeleteUser);// DELETE: /users/{id}
app.MapGet("/", () => "Backend Developer Technical Test!");
// Start App
app.Run();

static async Task<IResult> GetAllUsers(UserDb db)
{
    return TypedResults.Ok(await db.Users.ToListAsync());
}

static async Task<IResult> GetUser(int id, UserDb db)
{
    return await db.Users.FindAsync(id)
        is User user
            ? TypedResults.Ok(user)
            : TypedResults.NotFound();
}

static async Task<IResult> CreateUser(User user, UserDb db)
{
    db.Users.Add(user);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/users/{user.Id}", user);
}

static async Task<IResult> UpdateUser(int id, User inputUser, UserDb db)
{
    var user = await db.Users.FindAsync(id);
    if (user is null) return TypedResults.NotFound();

    user.Name = inputUser.Name;
    user.Birthdate = inputUser.Birthdate;
    user.Active = inputUser.Active;

    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

static async Task<IResult> DeleteUser(int id, UserDb db)
{
    var user = await db.Users.FindAsync(id);
    if (user is null) return TypedResults.NotFound();

    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return TypedResults.Ok();
}
