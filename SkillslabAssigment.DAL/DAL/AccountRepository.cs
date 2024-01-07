using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.DAL
{
    public class AccountRepository : GenericRepository<Account, short>, IAccountRepository
    {
        public AccountRepository(DbConnection connection) : base(connection)
        {
        }
        public async Task<Account> GetByEmailAsync(string email)
        {
            return (await _connection
                .SelectWhereAsync<Account>("email = @Email", new { Email = email }))
                .FirstOrDefault();
        }

        public Task<string> GetEmailByUserIdAsync(short userId)
        {
            const string GET_USER_ID_BY_EMAIL_QUERY = @"
                SELECT a.email FROM account a 
                INNER JOIN [user] u ON a.id = u.account_id
                WHERE u.id = @UserId
                ";
            return _connection.ExecuteScalarAsync<string>(GET_USER_ID_BY_EMAIL_QUERY, new { UserId = userId });
        }

        public async Task<bool> IsAuthenticated(string email, string password)
        {
            const string AUTHENTICATION_QUERY = @"
            SELECT * FROM account WHERE email = @Email AND 
            password = HASHBYTES('SHA2_256',@Password + CAST(salt AS NVARCHAR(MAX)))";
            IEnumerable<Account> accounts = await _connection.ExecuteQueryAsync<Account>(AUTHENTICATION_QUERY, new { Email = email, Password = password });
            return accounts.Any();
        }
    }
}
