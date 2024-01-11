using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.DAL
{
    public class WebPageElementRepository : GenericRepository<WebPageElement, int>, IWebPageElementRepositoty
    {
        public WebPageElementRepository(DbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<WebPageElement>> GetAllByUserIdAndWebPageNameAsync(short userId, string webPageName)
        {
            const string GET_ALL_BY_USER_ID_AND_WEB_PAGE_ID = @"
                SELECT pe.*
                FROM role_page_element_permission rp
                INNER JOIN user_role ur ON rp.role_id = ur.role_id
                INNER JOIN [user] u ON u.id = ur.user_id
                INNER JOIN page_element pe ON pe.id = rp.page_element_id
                INNER JOIN page_element_relations per ON per.page_element_id = rp.page_element_id
                INNER JOIN web_page wp ON per.web_page_id = wp.id
                WHERE u.id = @UserId AND wp.web_page_name = @WebPageName;
            ";
            return await _connection.ExecuteQueryAsync<WebPageElement>(GET_ALL_BY_USER_ID_AND_WEB_PAGE_ID, new { UserId = userId, WebPageName = webPageName });
        }

        public Task<IEnumerable<WebPageElement>> GetAllByWebPageIdAsync(int webPageId)
        {
            const string GET_ALL_BY_WEB_PAGE_ID = @"
                SELECT pe.*
                FROM page_element pe
                INNER JOIN page_element_relations per ON per.page_element_id = pe.id
                INNER JOIN web_page wp ON per.web_page_id = wp.id
                WHERE wp.id = @WebPageId;
            ";
            return _connection.ExecuteQueryAsync<WebPageElement>(GET_ALL_BY_WEB_PAGE_ID, new { WebPageId = webPageId });
        }
    }
}
