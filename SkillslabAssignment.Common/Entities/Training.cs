using SkillslabAssignment.Common.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.Entities
{
    [Table("training")]
    public class Training : IEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("location")]
        public string Location { get; set; }
        [Column("deadline")]
        public DateTime Deadline { get; set; }
        [Column("department_id")]
        public int DepartmentId { get; set; }
    }
}
