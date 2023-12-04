using SkillslabAssignment.Common.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillslabAssignment.Common.Entities
{
    [Table("attachment")]
    public class Attachment : IEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("url")]
        public string Url { get; set; }
        [Column("enrollment_id")]
        public int EnrollmentId { get; set; }
        [Column("prerequisite_id")]
        public int PrerequisiteId { get; set; }
    }
}
