using SkillslabAssignment.Common.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.Entities
{
    [Table("page_element_relations")]
    public class PageElementRelation : IEntity<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("web_page_id")]
        public int WebPageId { get; set; }
        [Column("page_element_id")]
        public int PageElementId { get; set; }
    }
}
