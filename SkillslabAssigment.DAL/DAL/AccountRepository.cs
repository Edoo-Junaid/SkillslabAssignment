using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.DAL
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public const string AUTHENTICATION_QUERY = "SELECT * FROM Account WHERE email = @Email AND password = @Password";

        public const string GET_BY_EMAIL_QUERY = "SELECT * FROM Account WHERE email = @Email";

        public AccountRepository(IDbConnection connection) : base(connection)
        {
        }

        public Account GetByEmail(string email)
        {
            return _connection.SelectWhere<Account>("email = @Email", new { Email = email }).FirstOrDefault();
        }

        public bool IsAuthenticated(string email, string password)
        {
            IEnumerable<Account> accounts = _connection.ExecuteQuery<Account>(AUTHENTICATION_QUERY, new { Email = email, Password = password });
            if (accounts.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
