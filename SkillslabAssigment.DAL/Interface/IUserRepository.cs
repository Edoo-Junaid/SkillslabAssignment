using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IEnumerable<User> GetUsersByDepartmentAndRole(int departmentId, string roleName);
        User GetByAccountId(int accountId);
        bool CreateUser(UserDto user);
        bool IsNicUnique(string nic);
    }
}
