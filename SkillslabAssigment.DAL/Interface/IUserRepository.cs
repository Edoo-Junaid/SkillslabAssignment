using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IEnumerable<User> GetUsersByDepartmentAndRole(int departmentId, string roleName);

        User GetByAccountId(int accountId);

        void CreateUser(CreateUserDTO createUserDTO, PendingAccount pendingAccount);
    }
}
