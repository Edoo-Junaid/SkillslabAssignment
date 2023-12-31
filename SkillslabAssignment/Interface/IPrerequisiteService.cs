using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface IPrerequisiteService : IGenericService<Prerequisite, short>
    {
        Task<IEnumerable<Prerequisite>> GetAllByTrainingIdAsync(short trainingId);
    }
}
