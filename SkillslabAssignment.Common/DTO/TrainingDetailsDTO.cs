using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;

namespace SkillslabAssignment.Common.DTO
{
    public class TrainingDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string DepartmentName { get; set; }
        public byte DepartmentId { get; set; }
        public IEnumerable<Prerequisite> Prerequisites { get; set; }
    }
}
