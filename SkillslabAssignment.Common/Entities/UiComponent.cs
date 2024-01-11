using SkillslabAssignment.Common.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.Entities
{
    [Table("ui_component")]
    public class UiComponent : IEntity<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("ui_component_name")]
        public string UiComponentName { get; set; }
        [Column("web_page_id")]
        public int WebPageId { get; set; }
    }
}
