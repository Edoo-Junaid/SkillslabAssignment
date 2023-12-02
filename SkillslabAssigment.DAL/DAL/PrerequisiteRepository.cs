using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Data;

namespace SkillslabAssigment.DAL.DAL
{
    public class PrerequisiteRepository : GenericRepository<Prerequisite>, IPrerequisiteRepository
    {
        public PrerequisiteRepository(IDbConnection connection) : base(connection)
        {
        }
        public IEnumerable<Prerequisite> GetAllByTrainingId(int trainingId)
        {
            const string GET_ALL_BY_TRAINING_ID_Query = "SELECT p.* FROM prerequisite p JOIN training_prerequisite tp ON p.id = tp.id WHERE tp.training_id = @TrainingId;";
            return _connection.ExecuteQuery<Prerequisite>(GET_ALL_BY_TRAINING_ID_Query, new { TrainingId = trainingId });
        }
    }
}
