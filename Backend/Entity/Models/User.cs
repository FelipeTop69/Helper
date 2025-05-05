namespace Entity.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Active { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;

        public List<UserRole> UserRoles { get; set; } = []; 

    }
}
