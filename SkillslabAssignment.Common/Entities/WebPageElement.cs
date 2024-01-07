using SkillslabAssignment.Common.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.Entities
{
    [Table("page_element")]
    public class WebPageElement : IEntity<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("page_element_name")]
        public string PageElementName { get; set; }
    }
}
