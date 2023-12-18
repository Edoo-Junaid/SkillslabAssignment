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
    public class RoleRepository : GenericRepository<Role, byte>, IRoleRepository
    {
        public RoleRepository(IDbConnection connection) : base(connection)
        {
        }

        public IEnumerable<Role> GetByUserId(short userId)
        {
            const string GET_BY_USER_ID_QUERY = @"
                SELECT role.* FROM user_role
                INNER JOIN [role] ON role.id = user_role.role_id
                WHERE user_role.user_id = @UserId
            ";
            return _connection.ExecuteQuery<Role>(GET_BY_USER_ID_QUERY, new { UserId = userId });
        }
    }
}
