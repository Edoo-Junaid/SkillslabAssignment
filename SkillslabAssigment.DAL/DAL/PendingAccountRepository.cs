using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.DAL;
using SkillslabAssignment.Common.Entities;
using System.Data;
using System.Linq;

namespace SkillslabAssigment.DAL.Interface
{
    public class PendingAccountRepository : GenericRepository<PendingAccount>, IPendingAccountRepository
    {
        public PendingAccountRepository(IDbConnection connection) : base(connection)
        {
        }
        public bool IsEmailUnique(string email)
        {
            return !_connection
                .SelectWhere<PendingAccount>("email = @Email", new { Email = email })
                .Any();
        }
        public bool IsNicUnique(string nic)
        {
            return !_connection
                .SelectWhere<PendingAccount>("nic = @Nic", new { Nic = nic })
                .Any();
        }
    }
}
