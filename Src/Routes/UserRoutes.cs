using UserStore.Models;
using Microsoft.EntityFrameworkCore;

public class UserRoutes
{
    public void MapUserRoutes(WebApplication app)
    {
        var users = app.MapGroup("/users");

        users.MapGet("/", GetAllUsers);      // GET:    /users
        users.MapGet("/{id}", GetUserById);  // GET:    /users/{id}
        users.MapPost("/", CreateUser);      // POST:   /users
        users.MapPut("/{id}", UpdateUser);   // PUT:    /users/{id}
        users.MapDelete("/{id}", DeleteUser);// DELETE: /users/{id}
    }

    private static async Task<IResult> GetAllUsers(UserDb db)
    {
        return TypedResults.Ok(await db.Users.ToListAsync());
    }

    private static async Task<IResult> GetUserById(int id, UserDb db)
    {
        return await db.Users.FindAsync(id)
            is User user
                ? TypedResults.Ok(user)
                : TypedResults.NotFound();
    }

    private static async Task<IResult> CreateUser(User user, UserDb db)
    {
        db.Users.Add(user);
        await db.SaveChangesAsync();
        return TypedResults.Created($"/users/{user.Id}", user);
    }

    private static async Task<IResult> UpdateUser(int id, User inputUser, UserDb db)
    {
        var user = await db.Users.FindAsync(id);
        if (user is null) return TypedResults.NotFound();

        user.Name = inputUser.Name;
        user.Birthdate = inputUser.Birthdate;
        user.Active = inputUser.Active;

        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    private static async Task<IResult> DeleteUser(int id, UserDb db)
    {
        var user = await db.Users.FindAsync(id);
        if (user is null) return TypedResults.NotFound();

        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return TypedResults.Ok();
    }
}
