using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IWebPageElementRepositoty : IGenericRepository<WebPageElement, int>
    {
        Task<IEnumerable<WebPageElement>> GetAllByUserIdAndWebPageNameAsync(short userId, string webPageName);
        Task<IEnumerable<WebPageElement>> GetAllByWebPageIdAsync(int webPageId);
    }
}
