using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface IEnrollementService : IGenericService<Enrollement, int>
    {
        Task<bool> ProcessEnrollementAsync(EnrollementRequestDTO enrollementRequest);
        Task<EnrollementRequestDTO> ProcessMultipartContentAsync(MultipartMemoryStreamProvider provider);
        Task<IEnumerable<EnrollementDTO>> GetAllByManagerIdAsync(short managerId);
    }
}
