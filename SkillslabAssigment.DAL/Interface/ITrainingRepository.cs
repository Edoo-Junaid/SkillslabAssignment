using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;

namespace SkillslabAssigment.DAL.Interface
{
    public interface ITrainingRepository : IGenericRepository<Training, short>
    {
        IEnumerable<Training> GetAllEnrolledTraining(short userId);
        bool CreateTraining(CreateTrainingRequestDTO training);
    }
}
