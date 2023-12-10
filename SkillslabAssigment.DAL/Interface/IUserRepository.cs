using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IUserRepository : IGenericRepository<User, short>
    {
        IEnumerable<User> GetUsersByDepartmentAndRole(byte departmentId, string roleName);
        User GetByAccountId(short accountId);
        bool CreateUser(CreateUserDTO user);
        bool IsNicUnique(string nic);
    }
}
