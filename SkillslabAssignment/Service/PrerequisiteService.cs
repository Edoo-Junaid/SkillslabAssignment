using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class PrerequisiteService : GenericService<Prerequisite, short>, IPrerequisiteService
    {
        private readonly IPrerequisiteRepository _prerequisiteRepository;
        public PrerequisiteService(IPrerequisiteRepository prerequisiteRepository) : base(prerequisiteRepository)
        {
            _prerequisiteRepository = prerequisiteRepository;
        }
        public async Task<IEnumerable<Prerequisite>> GetAllByTrainingIdAsync(short trainingId)
        {
            return await _prerequisiteRepository.GetAllByTrainingIdAsync(trainingId);
        }
    }
}
