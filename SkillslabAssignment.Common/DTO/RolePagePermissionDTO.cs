using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.DTO
{
    public class RolePagePermissionDTO
    {
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public List<WebPageElementPermissionDTO> WebPageElementPermissionDTO { get; set; }
    }
}
