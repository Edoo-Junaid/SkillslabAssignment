using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Collections.Generic;

namespace SkillslabAssignment.Interface
{
    public interface IUserService : IGenericService<User>
    {
        IEnumerable<ManagerDTO> GetAllManagerByDepartment(int departmentId);
        bool CreateUserAndAccount(CreateUserDTO createUserDTO);
        bool IsNicUnique(string nic);
    }
}
