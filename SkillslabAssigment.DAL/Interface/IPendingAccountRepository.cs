using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;

namespace SkillslabAssigment.DAL.DAL
{
    public interface IPendingAccountRepository : IGenericRepository<PendingAccount>
    {
        bool IsEmailUnique(string email);
        bool IsNicUnique(string nic);
    }
}
