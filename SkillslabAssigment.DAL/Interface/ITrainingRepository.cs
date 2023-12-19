using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public interface ITrainingRepository : IGenericRepository<Training, short>
    {
        Task<IEnumerable<Training>> GetAllEnrolledTrainingAsync(short userId);
        Task<bool> CreateTrainingAsync(CreateTrainingRequestDTO training);
    }
}
