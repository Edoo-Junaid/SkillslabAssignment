using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Notification
{
    public class InAppNotificationHandler : INotificationHandler
    {
        private readonly IInAppNotificationRepository _inAppNotificationRepository;

        public InAppNotificationHandler(IInAppNotificationRepository inAppNotificationRepository)
        {
            _inAppNotificationRepository = inAppNotificationRepository;
        }

        public async Task HandleNotificationAsync(string subject, string body, short userId)
        {
            InAppNotification inAppNotification = new InAppNotification()
            {
                Message = subject,
                Body = body,
                CreatedAt = DateTime.Now,
                IsRead = false,
                UserId = userId
            };
            await _inAppNotificationRepository.AddAsync(inAppNotification);
        }
    }
}
