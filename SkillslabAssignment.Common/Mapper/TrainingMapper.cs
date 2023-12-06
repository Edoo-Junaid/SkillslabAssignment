using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.Mapper
{
    public static class TrainingMapper
    {
        public static TrainingDTO ToDTO(this Training training)
        {
            return new TrainingDTO
            {
                Id = training.Id,
                Name = training.Name,
                Deadline = training.Deadline
            };
        }
        public static TrainingDetailsDTO ToDetailsDTO(this Training training, Department department, IEnumerable<Prerequisite> prerequisites)
        {
            return new TrainingDetailsDTO
            {
                Id = training.Id,
                Name = training.Name,
                Deadline = training.Deadline,
                Description = training.Description,
                Date = training.Date,
                Location = training.Location,
                DepartmentName = department.Name,
                Prerequisites = prerequisites
            };
        }
    }
}
