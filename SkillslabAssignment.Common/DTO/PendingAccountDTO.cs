using System.ComponentModel.DataAnnotations.Schema;

namespace SkillslabAssignment.Common.DTO
{
    [Table("pending_account")]
    public class PendingAccountDTO
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        [Column("nic")]
        public string Nic { get; set; }
    }
}
