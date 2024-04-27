namespace UserStore.Models
{
    public class User {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateOnly Birthdate { get; set; }
        public bool Active { get; set; }
    }
}
