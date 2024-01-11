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
    public class UiComponentService : GenericService<UiComponent, int>, IUiComponentService
    {
        private readonly IUiComponentRepository _uiComponentRepository;
        public UiComponentService(IUiComponentRepository repository) : base(repository)
        {
            _uiComponentRepository = repository;
        }

        public async Task<IEnumerable<UiComponent>> GetAllByUserIdAndWebPageNameAsync(short userId, string webPageName)
        {
            return await _uiComponentRepository.GetAllByUserIdAndWebPageNameAsync(userId, webPageName);
        }

        public async Task<IEnumerable<UiComponent>> GetAllByWebPageIdAsync(int webPageId)
        {
            return await _uiComponentRepository.GetAllByWebPageIdAsync(webPageId);
        }
    }
}
