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
    public class WebPageService : GenericService<WebPage, int>, IWebPageService
    {
        private readonly IWebPageRepository _webPageRepository;
        public WebPageService(IWebPageRepository repository) : base(repository)
        {
            _webPageRepository = repository;
        }

        public async Task<IEnumerable<WebPage>> GetAllByUserIdAsync(short userId)
        {
            return await _webPageRepository.GetAllByUserIdAsync(userId);
        }
    }
}
