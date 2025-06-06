﻿namespace Entity.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public bool Active { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
