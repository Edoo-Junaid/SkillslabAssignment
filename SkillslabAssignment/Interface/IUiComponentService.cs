using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface IUiComponentService : IGenericService<UiComponent, int>
    {
        Task<IEnumerable<UiComponent>> GetAllByUserIdAndWebPageNameAsync(short userId, string webPageName);
        Task<IEnumerable<UiComponent>> GetAllByWebPageIdAsync(int webPageId);
    }
}
