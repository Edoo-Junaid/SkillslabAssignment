using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.DAL
{
    public interface IPendingAccountRepository : IGenericRepository<PendingAccount, short>
    {
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsNicUniqueAsync(string nic);
        Task<bool> CreatePendingAccountAsync(PendingAccount pendingAccount);
        Task<IEnumerable<PendingAccountDTO>> GetAllPendingAccountDTOAsync();

    }
}
