using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface IInAppNotificationService : IGenericService<InAppNotification, int>
    {
        Task<IEnumerable<InAppNotification>> GetAllByUserIdAsync(short userId);
        Task<bool> MarkNotificationAsRead(int id);
        Task<int> GetUnReadCount(short userId);
    }
}
