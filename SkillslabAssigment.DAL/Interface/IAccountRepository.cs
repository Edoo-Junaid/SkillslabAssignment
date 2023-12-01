using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        bool IsAuthenticated(string email, string password);

        Account GetByEmail(string email);
    }
}
