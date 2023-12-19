using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface IUserService : IGenericService<User, short>
    {
        Task<IEnumerable<ManagerDTO>> GetAllManagerByDepartmentAsync(byte departmentId);
        Task<bool> CreateUserAndAccountAsync(CreateUserDTO createUserDTO);
        Task<bool> IsNicUniqueAsync(string nic);
    }
}
