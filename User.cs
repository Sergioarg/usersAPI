using Microsoft.EntityFrameworkCore;
using UsersStore.DB;

namespace UserStore.Models
{
    public class User {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateOnly Birthdate { get; set; }
        public bool Active { get; set; }
    }
}

class UserDb : DbContext
{
    public UserDb(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; } = null!;
}
