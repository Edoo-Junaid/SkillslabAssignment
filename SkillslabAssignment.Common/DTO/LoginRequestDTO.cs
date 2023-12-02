using System.ComponentModel.DataAnnotations;

namespace SkillslabAssignment.Common.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
