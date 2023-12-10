using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Collections.Generic;

namespace SkillslabAssignment.Interface
{
    public interface IPrerequisiteService : IGenericService<Prerequisite, short>
    {
        IEnumerable<Prerequisite> GetAllByTrainingId(short trainingId);
    }
}
