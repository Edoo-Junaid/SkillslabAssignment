using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IUserRepository : IGenericRepository<User, short>
    {
        Task<IEnumerable<User>> GetUsersByDepartmentAndRoleAsync(byte departmentId, string roleName);
        Task<User> GetByAccountIdAsync(short accountId);
        Task<bool> CreateUserAsync(CreateUserDTO user);
        Task<bool> IsNicUniqueAsync(string nic);
    }
}
