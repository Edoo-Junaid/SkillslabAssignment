using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IInAppNotificationRepository : IGenericRepository<InAppNotification, int>
    {
        Task<IEnumerable<InAppNotification>> GetAllByUserIdAsync(short userId);
        Task<bool> MarkNotificationAsRead(int id);
        Task<int> GetUnReadCount(short userId);
    }
}
