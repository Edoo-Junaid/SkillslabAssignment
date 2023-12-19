using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IEnrollmentRepository : IGenericRepository<Enrollement, int>
    {
        Task<IEnumerable<EnrollementDTO>> GetAllByManagerIdAsync(short managerId);
    }
}