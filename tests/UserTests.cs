using Xunit;
using UserStore.Models;

namespace UserStore.Tests
{
    public class UserTests
    {
        [Fact]
        public void User_ShouldHaveDefaultValues()
        {
            // Arrange
            var user = new User();

            // Assert
            Assert.Equal(0, user.Id);
            Assert.Null(user.Name);
            Assert.Equal(DateOnly.MinValue, user.Birthdate);
            Assert.False(user.Active);
        }
    }
}
