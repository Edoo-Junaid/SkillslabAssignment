using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System.Collections.Generic;

namespace SkillslabAssignment.Service
{
    public class PrerequisiteService : GenericService<Prerequisite>, IPrerequisiteService
    {
        private readonly IPrerequisiteRepository _prerequisiteRepository;
        public PrerequisiteService(IPrerequisiteRepository prerequisiteRepository) : base(prerequisiteRepository)
        {
            _prerequisiteRepository = prerequisiteRepository;
        }
        public IEnumerable<Prerequisite> GetAllByTrainingId(int trainingId)
        {
            return _prerequisiteRepository.GetAllByTrainingId(trainingId);
        }
    }
}
