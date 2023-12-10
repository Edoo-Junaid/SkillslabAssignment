using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;

namespace SkillslabAssigment.DAL.DAL
{
    public interface IPendingAccountRepository : IGenericRepository<PendingAccount, short>
    {
        bool IsEmailUnique(string email);
        bool IsNicUnique(string nic);
        bool CreatePendingAccount(PendingAccount pendingAccount);
        IEnumerable<PendingAccountDTO> GetAllPendingAccountDTO();

    }
}
