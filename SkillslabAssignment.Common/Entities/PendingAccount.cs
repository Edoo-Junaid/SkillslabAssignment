using SkillslabAssignment.Common.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.Entities
{
    [Table("pending_account")]
    public class PendingAccount : IEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "NIC is required")]
        [Column("nic")]
        public string Nic { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Column("password")]
        public string Password { get; set; }
        [Column("first_name")]

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Column("last_name")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        [Column("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Column("phone_number")]
        public string PhoneNumber { get; set; }

    }
}
