﻿using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SkillslabAssigment.DAL.DAL
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IDbConnection connection) : base(connection)
        {
        }
        public IEnumerable<User> GetUsersByDepartmentAndRole(int departmentId, string roleName)
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
        public User GetByAccountId(int accountId)
        {
            return _connection.SelectWhere<User>("account_id = @AccountId", new { AccountId = accountId }).FirstOrDefault();
        }
        public bool CreateUser(UserDto user)
        {
            const string CREATE_USER_TRANSACTION_QUERY = @"
                DECLARE @AccountId INT;
                INSERT INTO account (email,[password]) VALUES (@Email,@Password);
                SET @AccountId = SCOPE_IDENTITY();
                INSERT INTO [user] (first_name,last_name,nic,phone_number,department_id,role_id,manager_id,account_id)
                VALUES (@FirstName,@LastName,@Nic,@PhoneNumber,@DepartmentId,@RoleId,@ManagerId,@AccountId);
                DELETE FROM pending_account WHERE id = @PendingAccountId;
            ";
            return _connection.ExecuteTransaction(CREATE_USER_TRANSACTION_QUERY, user);
        }
        public bool IsNicUnique(string nic) => !_connection
            .SelectWhere<User>("nic = @Nic", new { Nic = nic })
            .Any();
    }
}
