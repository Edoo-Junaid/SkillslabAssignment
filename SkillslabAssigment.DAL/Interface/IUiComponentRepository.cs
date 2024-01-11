using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IUiComponentRepository : IGenericRepository<UiComponent, int>
    {
        Task<IEnumerable<UiComponent>> GetAllByUserIdAndWebPageNameAsync(short userId, string webPageName);
        Task<IEnumerable<UiComponent>> GetAllByWebPageIdAsync(int webPageId);
    }
}
