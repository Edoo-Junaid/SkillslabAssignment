using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;

namespace SkillslabAssignment.Common.Mapper
{
    public static class PendingAccountMapper
    {
        public static PendingAccountDTO pendingAccountToPendingAccountDTO(PendingAccount pendingAccount)
        {
            return new PendingAccountDTO() { Email = pendingAccount.Email, Id = pendingAccount.Id, Name = $"{pendingAccount.FirstName} {pendingAccount.LastName}", PhoneNumber = pendingAccount.PhoneNumber, Nic = pendingAccount.Nic };
        }
    }
}
