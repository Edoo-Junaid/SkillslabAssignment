using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IPrerequisiteRepository : IGenericRepository<Prerequisite, short>
    {
        Task<IEnumerable<Prerequisite>> GetAllByTrainingIdAsync(short trainingId);
    }
}
