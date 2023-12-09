using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Collections.Generic;

namespace SkillslabAssignment.Interface
{
    public interface IPendingAccountService : IGenericService<PendingAccount>
    {
        IEnumerable<PendingAccountDTO> GetAllPendingAccountDTOs();
        bool IsEmailUnique(string email);
        bool IsNicUnique(string nic);
    }
}
