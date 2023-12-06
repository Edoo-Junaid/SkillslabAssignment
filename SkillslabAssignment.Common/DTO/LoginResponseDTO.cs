namespace SkillslabAssignment.Common.DTO
{
    public class LoginResponseDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string AuthToken { get; set; }
    }
}
