using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.DAL
{
    public class UserRepository : GenericRepository<User, short>, IUserRepository
    {
        public UserRepository(DbConnection connection) : base(connection)
        {
        }
        public async Task<IEnumerable<User>> GetUsersByDepartmentAndRoleAsync(byte departmentId, string roleName)
        {
            const string GET_USERS_BY_DEPARTMENT_ROLE_QUERY = @"
            SELECT u.*
            FROM [user] u
            JOIN user_role ur ON u.id = ur.user_id
            WHERE u.department_id = @DepartmentId
            AND ur.role_id = (SELECT id FROM [role] WHERE name = @RoleName);
        ";
            var parameters = new { DepartmentId = departmentId, RoleName = roleName };
            return await _connection.ExecuteQueryAsync<User>(GET_USERS_BY_DEPARTMENT_ROLE_QUERY, parameters);
        }
        public async Task<User> GetByAccountIdAsync(short accountId)
        {
            return (await _connection
                .SelectWhereAsync<User>("account_id = @AccountId", new { AccountId = accountId }))
                .FirstOrDefault();
        }
        public async Task<bool> CreateUserAsync(CreateUserDTO user)
        {
            const string CREATE_USER_TRANSACTION_QUERY = @"
                DECLARE @AccountId TABLE (ID INT)
                DECLARE @UserId TABLE (ID INT)
                INSERT INTO account (email, [password], salt)
                OUTPUT inserted.id INTO @AccountId(ID)
                SELECT
                    email,
                    [password],
                    salt
                FROM pending_account
                WHERE id = @PendingAccountId;
                DECLARE @NewAccountId INT
                SELECT @NewAccountId = ID FROM @AccountId
                INSERT INTO [user] (nic, first_name, last_name, department_id, manager_id, account_id, phone_number)
                OUTPUT inserted.id INTO @UserId(ID)
                SELECT
                    nic,
                    first_name,
                    last_name,
                    @DepartmentId,
                    @ManagerId,
                    @NewAccountId,
                    phone_number
                FROM pending_account
                WHERE id = @PendingAccountId;

                DELETE FROM pending_account WHERE id = @PendingAccountId;

                DECLARE @NewUserId INT
                SELECT @NewUserId = ID FROM @UserId
                INSERT INTO user_role (user_id, role_id) VALUES (@NewUserId, @RoleId);
            ";
            return await _connection.ExecuteTransactionAsync(CREATE_USER_TRANSACTION_QUERY, user);
        }
        public async Task<bool> IsNicUniqueAsync(string nic) => !await _connection
            .RowExistsAsync<User>("nic = @Nic", new { Nic = nic });
    }
}
