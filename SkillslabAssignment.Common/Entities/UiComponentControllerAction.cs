using SkillslabAssignment.Common.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.Entities
{
    [Table("ui_component_controller_action")]
    public class UiComponentControllerAction : IEntity<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("ui_component_id")]
        public int UiComponentId { get; set; }
        [Column("permission_id")]
        public int PermissionId { get; set; }
    }
}
