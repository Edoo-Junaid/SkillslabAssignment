using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Notification
{
    public interface INotificationManager
    {
        void AddHandler(INotificationHandler handler);
        void RemoveHandler(INotificationHandler handler);
        Task NotifyHandlers(string subject, string body, short userId);
    }
}
