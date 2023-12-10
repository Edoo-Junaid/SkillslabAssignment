using SkillslabAssignment.Common.Attribute;
using SkillslabAssignment.Common.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillslabAssignment.Common.Entities
{
    [Table("account")]
    public class Account : IEntity<short>
    {
        [Column("id")]
        public short Id { get; set; }
        [Column("email")]
        public string Email { get; set; }
    }
}
