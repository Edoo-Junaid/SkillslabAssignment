using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.DAL
{
    public class WebPageRepository : GenericRepository<WebPage, int>, IWebPageRepository
    {
        public WebPageRepository(DbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<WebPage>> GetAllByUserIdAsync(short userId)
        {
            const string GET_ALL_BY_USER_ID = @"
                SELECT web_page.* FROM role_page_permission
                INNER JOIN [role] on role.id =role_page_permission.role_id
                INNER JOIN web_page on web_page.id = role_page_permission.web_page_id
                INNER JOIN [user_role] on user_role.role_id = role.id
                INNER JOIN [user] on [user].id = user_role.user_id 
                WHERE [user].id = @UserId;
            ";
            return await _connection.ExecuteQueryAsync<WebPage>(GET_ALL_BY_USER_ID, new { UserId = userId });
        }
    }
}
