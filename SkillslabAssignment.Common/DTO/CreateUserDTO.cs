using System.ComponentModel.DataAnnotations;

namespace SkillslabAssignment.Common.DTO
{
    public class CreateUserDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Pending Account Id required")]
        public short PendingAccountId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Department Id is required")]
        public byte DepartmentId { get; set; }
        public short? ManagerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Role Id is required")]
        public byte RoleId { get; set; }
    }
}
