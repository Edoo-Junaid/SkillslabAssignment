using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface ITrainingService : IGenericService<Training, short>
    {
        Task<IEnumerable<TrainingDTO>> GetAllTrainingDTOAsync();
        Task<IEnumerable<Training>> GetAllWithSearchAsync(string searchValue);
        Task<TrainingDetailsDTO> GetTrainingDetailsAsync(short id);
        Task<IEnumerable<Training>> GetAllEnrolledTrainingAsync(short userId);
        Task<bool> CreteTrainingAsync(CreateTrainingRequestDTO training);
        Task<bool> UpdateTrainingAndPrerequisiteAsync(TrainingDetailsDTO training);
        Task<bool> DeleteTrainingAndPrerequisiteAsync(short trainingId);
    }
}
