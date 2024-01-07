using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IWebPageRepository : IGenericRepository<WebPage, int>
    {
        Task<IEnumerable<WebPage>> GetAllByUserIdAsync(short userId);
    }
}
