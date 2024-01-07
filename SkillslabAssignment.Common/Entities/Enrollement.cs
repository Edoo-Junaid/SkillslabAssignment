using SkillslabAssignment.Common.Interface;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillslabAssignment.Common.Entities
{
    [Table("enrollment")]
    public class Enrollement : IEntity<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public short UserId { get; set; }
        [Column("training_id")]
        public short TrainingId { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("status")]
        public string Status { get; set; }

        [Column("decline_reason")]
        public string DeclineReason { get; set; }=null;
    }
}
