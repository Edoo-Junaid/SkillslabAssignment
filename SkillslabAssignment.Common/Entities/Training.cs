using SkillslabAssignment.Common.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillslabAssignment.Common.Entities
{
    [Table("training")]
    public class Training : IEntity<short>
    {
        [Column("id")]
        public short Id { get; set; }
        [Column("name")]
        [Required(ErrorMessage = "Training Name Required")]
        public string Name { get; set; }
        [Column("description")]
        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }
        [Column("training_capacity")]
        [Required(ErrorMessage = "Training Capacity Required")]
        public short TrainingCapacity { get; set; }
        [Column("date")]
        [Required(ErrorMessage = "Date Required")]
        public DateTime Date { get; set; }
        [Column("location")]
        [Required(ErrorMessage = "Location Required")]
        public string Location { get; set; }
        [Column("deadline")]
        [Required(ErrorMessage = "Deadline Required")]
        public DateTime Deadline { get; set; }
        [Column("department_id")]
        [Required(ErrorMessage = "Department Id Required")]
        public byte DepartmentId { get; set; }
    }
}
