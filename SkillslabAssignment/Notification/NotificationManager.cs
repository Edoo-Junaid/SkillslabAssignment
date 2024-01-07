using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Notification
{
    public class NotificationManager : INotificationManager
    {
        private List<INotificationHandler> handlers = new List<INotificationHandler>();

        public NotificationManager(IEnumerable<INotificationHandler> notificationHandlers)
        {
            handlers = notificationHandlers.ToList();
        }
        public void AddHandler(INotificationHandler handler)
        {
            handlers.Add(handler);
        }

        public void RemoveHandler(INotificationHandler handler)
        {
            handlers.Remove(handler);
        }
        public async Task NotifyHandlers(string subject, string body, short userId)
        {
            foreach (var handler in handlers)
            {
               await handler.HandleNotificationAsync(subject, body, userId);
            }
        }
    }
}
