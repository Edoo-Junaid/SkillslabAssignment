using SkillslabAssigment.DAL.DAL;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Mapper;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class PendingAccountService : GenericService<PendingAccount, short>, IPendingAccountService
    {
        private readonly IPendingAccountRepository _pendingAccountRepository;
        public PendingAccountService(IPendingAccountRepository pendingAccountRepository) : base(pendingAccountRepository)
        {
            _pendingAccountRepository = pendingAccountRepository;
        }

        public async Task<bool> CreatePendingAccountAsync(PendingAccount pendingAccount)
        {
            return await _pendingAccountRepository.CreatePendingAccountAsync(pendingAccount);
        }

        public async Task<IEnumerable<PendingAccountDTO>> GetAllPendingAccountDTOsAsync()
        {
            return await _pendingAccountRepository.GetAllPendingAccountDTOAsync();
        }
        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return await _pendingAccountRepository.IsEmailUniqueAsync(email);
        }
        public async Task<bool> IsNicUniqueAsync(string nic)
        {
            return await _pendingAccountRepository.IsNicUniqueAsync(nic);
        }
    }
}
