using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Collections;
using System.Collections.Generic;

namespace SkillslabAssignment.Interface
{
    public interface ITrainingService : IGenericService<Training>
    {
        IEnumerable<TrainingDTO> GetAllTrainingDTO();
        TrainingDetailsDTO GetTrainingDetails(int id);
    }
}
