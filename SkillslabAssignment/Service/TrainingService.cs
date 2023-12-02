using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;

namespace SkillslabAssignment.Service
{
    public class TrainingService : GenericService<Training>, ITrainingService
    {
        public TrainingService(IGenericRepository<Training> repository) : base(repository)
        {
        }
    }
}
