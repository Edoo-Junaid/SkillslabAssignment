using SkillslabAssignment.Common.Entities;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IAccountRepository : IGenericRepository<Account, short>
    {
        bool IsAuthenticated(string email, string password);
        Account GetByEmail(string email);
    }
}
