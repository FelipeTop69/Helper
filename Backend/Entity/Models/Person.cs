namespace Entity.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public string BloodType { get; set; } = string.Empty;
        public bool Active { get; set; }

        public User User { get; set; } = null!;
    }
}
