using System.ComponentModel.DataAnnotations.Schema;

namespace SkillslabAssignment.Common.DTO
{
    public class UserDto
    {
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("nic")]
        public string NIC { get; set; }
        [Column("department_id")]
        public int DepartmentId { get; set; }
        [Column("manager_id")]
        public int? ManagerId { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        public int PendingAccountId { get; set; }
    }
}
