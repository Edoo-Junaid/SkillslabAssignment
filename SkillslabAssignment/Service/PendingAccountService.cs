using SkillslabAssigment.DAL.DAL;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Mapper;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.Linq;

namespace SkillslabAssignment.Service
{
    public class PendingAccountService : GenericService<PendingAccount, short>, IPendingAccountService
    {
        private readonly IPendingAccountRepository _pendingAccountRepository;
        public PendingAccountService(IPendingAccountRepository pendingAccountRepository) : base(pendingAccountRepository)
        {
            _pendingAccountRepository = pendingAccountRepository;
        }

        public bool CreatePendingAccount(PendingAccount pendingAccount)
        {
            return _pendingAccountRepository.CreatePendingAccount(pendingAccount);
        }

        public IEnumerable<PendingAccountDTO> GetAllPendingAccountDTOs()
        {
            return _pendingAccountRepository.GetAllPendingAccountDTO();
        }
        public bool IsEmailUnique(string email)
        {
            return _pendingAccountRepository.IsEmailUnique(email);
        }
        public bool IsNicUnique(string nic)
        {
            return _pendingAccountRepository.IsNicUnique(nic);
        }
    }
}
