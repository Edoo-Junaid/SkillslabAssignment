using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IPermissionRepository : IGenericRepository<Permission, short>
    {
        Task<bool> HasPermissionAsync(string userId, string permission);

        Task<IEnumerable<Permission>> GetAllByUserIdAsync(short userId);
    }
}
