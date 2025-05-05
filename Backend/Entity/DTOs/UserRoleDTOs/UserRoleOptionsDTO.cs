namespace Entity.DTOs.UserRoleDTOs
{
    public class UserRoleOptionsDTO
    {
        public int Id { get; set; }
        public bool Status { get; set; }

        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
