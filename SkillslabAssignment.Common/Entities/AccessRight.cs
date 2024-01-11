using SkillslabAssignment.Common.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.Entities
{
    [Table("access_right")]
    public class AccessRight : IEntity<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("web_page_id")]
        public int WebPageId { get; set; }
        [Column("ui_component_id")]
        public int UiComponentId { get; set; }
    }
}
