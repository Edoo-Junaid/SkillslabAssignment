using SkillslabAssignment.Common.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillslabAssignment.Common.Entities
{
    [Table("role")]
    public class Role : IEntity<byte>
    {
        [Column("id")]
        public byte Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
}
