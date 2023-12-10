using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Collections.Generic;

namespace SkillslabAssignment.Interface
{
    public interface ITrainingService : IGenericService<Training, short>
    {
        IEnumerable<TrainingDTO> GetAllTrainingDTO();
        TrainingDetailsDTO GetTrainingDetails(short id);
        IEnumerable<Training> GetAllEnrolledTraining(short userId);
        bool CreteTraining(CreateTrainingRequestDTO training);
    }
}
