using SkillslabAssignment.Common.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.Entities
{
    [Table("role_page_permission")]
    public class RolePagePermission : IEntity<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("web_page_id")]
        public int WebPageId { get; set; }
    }
}
