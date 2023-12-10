using SkillslabAssignment.Common.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillslabAssignment.Common.Entities
{
    [Table("user")]
    public class User : IEntity<short>
    {
        [Column("id")]
        public short Id { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("nic")]
        public string NIC { get; set; }
        [Column("department_id")]
        public byte DepartmentId { get; set; }
        [Column("manager_id")]
        public short? ManagerId { get; set; }
        [Column("account_id")]
        public short AccountId { get; set; }
        [Column("role_id")]
        public byte RoleId { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
    }
}
