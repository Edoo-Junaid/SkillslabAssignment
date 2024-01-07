using SkillslabAssignment.Common.Entities;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IAccountRepository : IGenericRepository<Account, short>
    {
        Task<bool> IsAuthenticated(string email, string password);
        Task<Account> GetByEmailAsync(string email);

        Task<string> GetEmailByUserIdAsync(short userId);
    }
}
