using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class WebPageElementService : GenericService<WebPageElement, int>, IWebPageElementService
    {
        private readonly IWebPageElementRepositoty _webPageElementRepositoty;
        public WebPageElementService(IWebPageElementRepositoty repository) : base(repository)
        {
            _webPageElementRepositoty = repository;
        }

        public async Task<IEnumerable<WebPageElement>> GetAllByUserIdAndWebPageNameAsync(short userId, string webPageName)
        {
            return await _webPageElementRepositoty.GetAllByUserIdAndWebPageNameAsync(userId, webPageName);
        }

        public async Task<IEnumerable<WebPageElement>> GetAllByWebPageIdAsync(int webPageId)
        {
            return await _webPageElementRepositoty.GetAllByWebPageIdAsync(webPageId);
        }
    }
}
