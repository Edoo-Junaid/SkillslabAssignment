using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.DAL
{
    public class PrerequisiteRepository : GenericRepository<Prerequisite>, IPrerequisiteRepository
    {
        private const string GET_ALL_BY_TRAINING_ID_Query = "SELECT p.* FROM prerequisite p JOIN training_prerequisite tp ON p.id = tp.id WHERE tp.training_id = @TrainingId;";
        public PrerequisiteRepository(IDbConnection connection) : base(connection)
        {
        }

        public IEnumerable<Prerequisite> GetAllByTrainingId(int trainingId)
        {
            return _connection.ExecuteQuery<Prerequisite>(GET_ALL_BY_TRAINING_ID_Query, new { TrainingId = trainingId });
        }
    }
}
