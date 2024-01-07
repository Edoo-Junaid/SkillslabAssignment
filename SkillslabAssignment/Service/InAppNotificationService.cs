using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class InAppNotificationService : GenericService<InAppNotification, int>, IInAppNotificationService
    {
        private readonly IInAppNotificationRepository _inAppNotificationRepository;
        public InAppNotificationService(IInAppNotificationRepository inAppNotificationRepository) : base(inAppNotificationRepository)
        {
            _inAppNotificationRepository = inAppNotificationRepository;
        }

        public async Task<IEnumerable<InAppNotification>> GetAllByUserIdAsync(short userId)
        {
            return await _inAppNotificationRepository.GetAllByUserIdAsync(userId);
        }

        public async Task<bool> MarkNotificationAsRead(int id)
        {
            return await _inAppNotificationRepository.MarkNotificationAsRead(id);
        }

        public async Task<int> GetUnReadCount(short userId)
        {
            return await _inAppNotificationRepository.GetUnReadCount(userId);
        }
    }
}
