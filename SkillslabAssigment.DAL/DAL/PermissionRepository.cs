using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.DAL
{
    public class PermissionRepository : GenericRepository<Permission, short>, IPermissionRepository
    {
        public PermissionRepository(DbConnection connection) : base(connection)
        {
        }

        public async Task<bool> HasPermissionAsync(string userId, string permissionName)
        {
            const string checkRolePermissionQuery = @"
                SELECT p.*
                FROM [user] u
                JOIN user_role ur ON u.id = ur.user_id
                JOIN role r ON ur.role_id = r.id
                JOIN role_permission rp ON r.id = rp.role_id
                JOIN permission p ON rp.permission_id = p.id
                WHERE u.id = @UserId AND p.permission_name = @PermissionName;
            ";

            var parameters = new { UserId = userId, PermissionName = permissionName };
            IEnumerable<Permission> permissions = await _connection.ExecuteQueryAsync<Permission>(checkRolePermissionQuery, parameters);
            return permissions.Any();
        }

        public async Task<IEnumerable<Permission>> GetAllByUserIdAsync(short userId)
        {
            const string getAllByUserIdQuery = @"
                SELECT p.*
                FROM [user] u
                JOIN user_role ur ON u.id = ur.user_id
                JOIN role r ON ur.role_id = r.id
                JOIN role_permission rp ON r.id = rp.role_id
                JOIN permission p ON rp.permission_id = p.id
                WHERE u.id = @UserId;
            ";
            var parameters = new { UserId = userId };
            return await _connection.ExecuteQueryAsync<Permission>(getAllByUserIdQuery, parameters);
        }
    }
}
