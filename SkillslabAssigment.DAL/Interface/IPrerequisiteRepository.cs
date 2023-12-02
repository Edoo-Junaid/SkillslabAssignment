using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IPrerequisiteRepository : IGenericRepository<Prerequisite>
    {
        IEnumerable<Prerequisite> GetAllByTrainingId(int trainingId);
    }
}
