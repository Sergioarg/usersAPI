using Microsoft.OpenApi.Models;
using UsersStore.DB;

var builder = WebApplication.CreateBuilder(args);
// Config Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config => {
    config.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Users API",
        Description = "Get and manage users data",
        Version = "v1"
    });
});

var app = builder.Build();


// Using Swagger
string swaggerPath = "/swagger/v1/swagger.json";

app.UseSwagger();
app.UseSwaggerUI(config => {
    config.SwaggerEndpoint(swaggerPath, "Users API V1");
});

app.MapGet("/users/{id}", (int id) => UserDB.GetUser(id));
app.MapGet("/users/", () => UserDB.GetUsers());
app.MapPost("/users", (User user) => UserDB.CreateUser(user));
app.MapPut("/users", (User user) => UserDB.UpdateUser(user));
app.MapDelete("/users/{id}", (int id) => UserDB.RemoveUser(id));

app.Run();
