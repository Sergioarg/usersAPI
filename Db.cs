namespace UsersStore.DB;

public record User {
    public int Id {get; set;}
    public string? Name {get; set;}
    public DateOnly Birthdate {get; set;}
    public bool Active {get; set;}
}

public class UserDB
{
    private static List<User> _users = new List<User>()
    {
        new User{
            Id=1,
            Name="Sergio Ramos",
            Active=true,
            Birthdate=DateOnly.FromDateTime(new DateTime(2001, 03, 28))
        },
        new User{
            Id=2,
            Name="William Rodriguez",
            Active=true,
            Birthdate=DateOnly.FromDateTime(new DateTime(1992, 08, 15))
        },
        new User{
            Id=3,
            Name="Mark Grayson",
            Active=false,
            Birthdate=DateOnly.FromDateTime(new DateTime(1992, 06, 28))
        }
    };

    public static List<User> GetUsers() {
        return _users;
    }

    public static User ? GetUser(int id) {
        return _users.SingleOrDefault(user => user.Id == id);
    }

    public static User CreatePizza(User user) {
        _users.Add(user);
        return user;
    }
    public static User UpdateUser(User update) {
        _users = _users.Select(user => {
            if (user.Id == update.Id)
            {
                user.Name = update.Name;
            }
            return user;
        }).ToList();
        return update;
    }
    public static void RemoveUser(int id) {
        _users = _users.FindAll(pizza => pizza.Id != id).ToList();
    }
}
