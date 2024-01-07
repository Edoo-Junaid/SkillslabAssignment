using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Notification
{
    public interface INotificationHandler
    {
        Task HandleNotificationAsync(string subject, string body, short userId);
    }
}
