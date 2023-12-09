using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;

namespace SkillslabAssigment.DAL.Interface
{
    public interface ITrainingRepository : IGenericRepository<Training>
    {
        IEnumerable<Training> GetAllEnrolledTraining(int userId);
        bool CreateTraining(CreateTrainingRequestDTO training);
    }
}
