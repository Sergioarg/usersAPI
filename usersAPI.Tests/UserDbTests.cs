using Xunit;
using UserStore.Models;
using Microsoft.EntityFrameworkCore;

namespace UserStore.Tests
{
    public class UserDbTests
    {
        [Fact]
        public void CanAddUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<UserDb>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Act
            using (var context = new UserDb(options))
            {
                context.Users.Add(new User {
                    Name = "Test User",
                    Birthdate = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                });
                context.SaveChanges();
            }

            // Assert
            using (var context = new UserDb(options))
            {
                Assert.Single(context.Users);
                Assert.Equal("Test User", context.Users.Single().Name);
            }
        }
    }
}
