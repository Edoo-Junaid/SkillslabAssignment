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
        public int Id { get; set; }
        public string WebPageName { get; set; }
    }
}
