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

// Using Users Routes
var userRoutes = new UserRoutes();
userRoutes.MapUserRoutes(app);

app.MapGet("/", () => "Backend Developer Technical Test!");
// Start App
app.Run();
