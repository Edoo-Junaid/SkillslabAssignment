using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.Common.Interface;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillslabAssignment.Common.Entities
{
    [Table("enrollment")]
    public class Enrollement : IEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("training_id")]
        public int TrainingId { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("status")]
        public string Status { get; set; }
    }
}
