using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.DAL;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public class PendingAccountRepository : GenericRepository<PendingAccount, short>, IPendingAccountRepository
    {
        public PendingAccountRepository(DbConnection connection) : base(connection)
        {
        }
        public async Task<bool> CreatePendingAccountAsync(PendingAccount pendingAccount)
        {
            const string CREATE_PENDING_ACCOUNT_QUERY = @"
                DECLARE @Salt UNIQUEIDENTIFIER = NEWID();
                INSERT INTO pending_account (nic, password, salt, first_name, last_name, email, phone_number)
                VALUES
                (@Nic, HASHBYTES('SHA2_256', @Password
                + CAST( @Salt AS NVARCHAR(MAX))),  @Salt, @FirstName,
                @LastName, @Email, @PhoneNumber)
            ";
            return await _connection.ExecuteTransactionAsync(CREATE_PENDING_ACCOUNT_QUERY, pendingAccount);
        }
        public async Task<IEnumerable<PendingAccountDTO>> GetAllPendingAccountDTOAsync()
        {
            return await _connection.GetAllAsync<PendingAccountDTO>();
        }
        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _connection
                .RowExistsAsync<PendingAccount>("email = @Email", new { Email = email });
        }
        public async Task<bool> IsNicUniqueAsync(string nic)
        {
            return !await _connection
                .RowExistsAsync<PendingAccount>("nic = @Nic", new { Nic = nic });
        }
    }
}
