using SkillslabAssignment.Common.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.Entities
{
    [Table("web_page")]
    public class WebPage : IEntity<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("web_page_name")]
        public string WebPageName { get; set; }
    }
}