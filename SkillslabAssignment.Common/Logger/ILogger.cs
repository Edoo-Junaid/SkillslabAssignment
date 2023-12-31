using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.Logger
{
    public interface ILogger
    {
        Task LogAsync(string message);
        Task LogAsync(string message, Exception exception);
        Task LogAsync(Exception exception);
    }
}
