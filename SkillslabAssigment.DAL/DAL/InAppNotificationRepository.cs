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
    public class InAppNotificationRepository : GenericRepository<InAppNotification, int>, IInAppNotificationRepository
    {
        public InAppNotificationRepository(DbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<InAppNotification>> GetAllByUserIdAsync(short userId)
        {
            return await _connection.SelectWhereAsync<InAppNotification>("user_id = @UserId ORDER BY created_at DESC ", new { UserId = userId });
        }

        public async Task<int> GetUnReadCount(short userId)
        {
            return await _connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM in_app_notification WHERE user_id = @UserId AND is_read = 0", new { UserId = userId });
        }

        public async Task<bool> MarkNotificationAsRead(int id)
        {
            return await _connection.UpdateByIdAsync<InAppNotification>(id, new { is_read = true });
        }
    }
}
