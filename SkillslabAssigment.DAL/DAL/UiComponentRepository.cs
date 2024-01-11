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
    public class UiComponentRepository : GenericRepository<UiComponent, int>, IUiComponentRepository
    {
        public UiComponentRepository(DbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<UiComponent>> GetAllByUserIdAndWebPageNameAsync(short userId, string webPageName)
        {
            const string GET_ALL_BY_USER_ID_AND_WEB_PAGE_ID = @"
                SELECT uc.*
                FROM access_right ar
                INNER JOIN [role] r ON ar.role_id = r.id
                INNER JOIN web_page wp ON wp.id = ar.web_page_id
                INNER JOIN ui_component uc ON ar.ui_component_id = uc.id
                INNER JOIN [user_role] ur ON ur.role_id = r.id
                INNER JOIN [user] u ON u.id = ur.user_id
                WHERE wp.web_page_name = @WebPageName AND u.id =@UserId;
            ";
            return await _connection.ExecuteQueryAsync<UiComponent>(GET_ALL_BY_USER_ID_AND_WEB_PAGE_ID, new { UserId = userId, WebPageName = webPageName });

        }

        public async Task<IEnumerable<UiComponent>> GetAllByWebPageIdAsync(int webPageId)
        {
            return await _connection.SelectWhereAsync<UiComponent>("web_page_id=@WebPageId", new
            {
                WebPageId = webPageId
            });
        }
    }
}
