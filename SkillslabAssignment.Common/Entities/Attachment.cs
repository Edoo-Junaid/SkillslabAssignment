using SkillslabAssignment.Common.Interface;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillslabAssignment.Common.Entities
{
    [Table("attachment")]
    public class Attachment : IEntity<short>
    {
        [Column("id")]
        public short Id { get; set; }
        [Column("file_id")]
        public Guid FileId { get; set; }
        [Column("enrollment_id")]
        public int EnrollmentId { get; set; }
        [Column("prerequisite_id")]
        public short PrerequisiteId { get; set; }
    }
}
