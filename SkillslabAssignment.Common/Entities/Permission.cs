using SkillslabAssignment.Common.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.Entities
{
    [Table("Permission")]
    public class Permission : IEntity<short>
    {
        [Column("id")]
        public short Id { get; set; }
        [Column("permission_name")]
        public string Name { get; set; }
    }
}
