using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SkillslabAssigment.DAL.DAL
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(IDbConnection connection) : base(connection)
        {
        }
        public Account GetByEmail(string email) => _connection
            .SelectWhere<Account>("email = @Email", new { Email = email })
            .FirstOrDefault();
        public bool IsAuthenticated(string email, string password)
        {
            const string AUTHENTICATION_QUERY = @"
            SELECT * FROM account WHERE email = @Email AND 
            password = HASHBYTES('SHA2_256',@Password + CAST(salt AS NVARCHAR(MAX)))";
            IEnumerable<Account> accounts = _connection.ExecuteQuery<Account>(AUTHENTICATION_QUERY, new { Email = email, Password = password });
            return accounts.Any();
        }
    }
}
