using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string email, string subject, string message);
    }
}
