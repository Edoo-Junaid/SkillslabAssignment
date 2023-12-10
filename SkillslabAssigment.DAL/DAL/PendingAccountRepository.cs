using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.DAL;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SkillslabAssigment.DAL.Interface
{
    public class PendingAccountRepository : GenericRepository<PendingAccount, short>, IPendingAccountRepository
    {
        public PendingAccountRepository(IDbConnection connection) : base(connection)
        {
        }

        public bool CreatePendingAccount(PendingAccount pendingAccount)
        {
            const string CREATE_PENDING_ACCOUNT_QUERY = @"
                DECLARE @Salt UNIQUEIDENTIFIER = NEWID();
                INSERT INTO pending_account (nic, password, salt, first_name, last_name, email, phone_number)
                VALUES
                (@Nic, HASHBYTES('SHA2_256', @Password
                + CAST( @Salt AS NVARCHAR(MAX))),  @Salt, @FirstName,
                @LastName, @Email, @PhoneNumber)
            ";
            return _connection.ExecuteTransaction(CREATE_PENDING_ACCOUNT_QUERY, pendingAccount);
        }

        public IEnumerable<PendingAccountDTO> GetAllPendingAccountDTO()
        {
            return _connection.GetAll<PendingAccountDTO>();
        }

        public bool IsEmailUnique(string email)
        {
            return !_connection
                .RowExists<PendingAccount>("email = @Email", new { Email = email });
        }
        public bool IsNicUnique(string nic)
        {
            return !_connection
                .RowExists<PendingAccount>("nic = @Nic", new { Nic = nic });
        }

    }
}
