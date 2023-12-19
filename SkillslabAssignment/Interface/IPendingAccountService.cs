using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface IPendingAccountService : IGenericService<PendingAccount, short>
    {
        Task<IEnumerable<PendingAccountDTO>> GetAllPendingAccountDTOsAsync();
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsNicUniqueAsync(string nic);
        Task<bool> CreatePendingAccountAsync(PendingAccount pendingAccount);
    }
}
