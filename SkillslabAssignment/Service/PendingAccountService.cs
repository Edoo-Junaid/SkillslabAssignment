using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Mapper;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.Linq;

namespace SkillslabAssignment.Service
{
    public class PendingAccountService : GenericService<PendingAccount>, IPendingAccountService
    {
        public PendingAccountService(IGenericRepository<PendingAccount> repository) : base(repository)
        {
        }
        public IEnumerable<PendingAccountDTO> GetAllPendingAccountDTOs()
        {
            return _repository.GetAll().Select(pendingAccount => pendingAccount.ToPendingAccountDTO());
        }
    }
}
