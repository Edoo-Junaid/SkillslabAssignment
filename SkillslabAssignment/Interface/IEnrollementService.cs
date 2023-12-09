using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface IEnrollementService : IGenericService<Enrollement>
    {
        Task<bool> ProcessEnrollement(EnrollementRequestDTO enrollementRequest);
        Task<EnrollementRequestDTO> ProcessMultipartContent(MultipartMemoryStreamProvider provider);
        IEnumerable<EnrollementDTO> GetAllByManagerId(int managerId);
    }
}
