using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface ITrainingService : IGenericService<Training, short>
    {
        Task<IEnumerable<TrainingDTO>> GetAllTrainingDTOAsync();
        Task<TrainingDetailsDTO> GetTrainingDetailsAsync(short id);
        Task<IEnumerable<Training>> GetAllEnrolledTrainingAsync(short userId);
        Task<bool> CreteTrainingAsync(CreateTrainingRequestDTO training);
    }
}
