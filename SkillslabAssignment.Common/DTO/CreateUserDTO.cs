using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.DTO
{
    public class CreateUserDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Pending Account Id required")]
        public int PendingAccountId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Department Id is required")]
        public int DepartmentId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Manager Id is required")]
        public int ManagerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Role Id is required")]
        public int RoleId { get; set; }
    }
}
