using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IPrerequisiteRepository : IGenericRepository<Prerequisite>
    {
        IEnumerable<Prerequisite> GetAllByTrainingId(int trainingId);
    }
}
