using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SkillslabAssigment.DAL.DAL
{
    public class UserRepository : GenericRepository<User, short>, IUserRepository
    {
        public UserRepository(IDbConnection connection) : base(connection)
        {
        }
        public IEnumerable<User> GetUsersByDepartmentAndRole(byte departmentId, string roleName)
        {
            const string GET_USERS_BY_DEPARTMENT_ROLE_QUERY = @"
                SELECT *
                FROM [user]
                WHERE department_id = @DepartmentId
                AND role_id = (SELECT id FROM [role] WHERE name = @RoleName);
            ";
            var parameters = new { DepartmentId = departmentId, RoleName = roleName };
            return _connection.ExecuteQuery<User>(GET_USERS_BY_DEPARTMENT_ROLE_QUERY, parameters);
        }
        public User GetByAccountId(short accountId)
        {
            return _connection.SelectWhere<User>("account_id = @AccountId", new { AccountId = accountId }).FirstOrDefault();
        }
        public bool CreateUser(CreateUserDTO user)
        {
            const string CREATE_USER_TRANSACTION_QUERY = @"
            DECLARE @AccountId TABLE (ID INT)

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
            INSERT INTO [user] (nic, first_name, last_name, role_id, department_id, manager_id, account_id, phone_number)
            SELECT
                nic,
                first_name,
                last_name,
                @RoleId,
                @DepartmentId,
                @ManagerId,
                @NewAccountId,
                phone_number
            FROM pending_account
            WHERE id = @PendingAccountId;

            DELETE FROM pending_account WHERE id = @PendingAccountId;
        ";
            return _connection.ExecuteTransaction(CREATE_USER_TRANSACTION_QUERY, user);
        }
        public bool IsNicUnique(string nic) => !_connection
            .RowExists<User>("nic = @Nic", new { Nic = nic });
    }
}
