using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface IPermissionService : IGenericService<Permission, short>
    {
        Task<bool> HasPermissionAsync(string userId, string permission);
        Task<IEnumerable<Permission>> GetAllByUserIdAsync(short userId);
    }
}
