using SkillslabAssignment.Common.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillslabAssignment.Common.Entities
{
    [Table("user")]
    public class User : IEntity
    {
        [Column("id")]
        public int Id { get; set; }
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
        [Column("account_id")]
        public int AccountId { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
    }
}
