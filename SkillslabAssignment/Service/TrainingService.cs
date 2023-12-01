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
    public class TrainingService : GenericService<Training>, ITrainingService
    {
        public TrainingService(IGenericRepository<Training> repository) : base(repository)
        {
        }
    }
}
