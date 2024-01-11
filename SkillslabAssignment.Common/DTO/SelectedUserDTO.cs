using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.DTO
{
    public class SelectedUserDTO
    {
        [Column("user_id")]
        public short UserId { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("manager_name")]
        public string ManagerName { get; set; }
    }
}
