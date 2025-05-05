namespace Entity.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool Active { get; set; }

        public List<UserRole> UserRoles { get; set;} = [];
    }
}
