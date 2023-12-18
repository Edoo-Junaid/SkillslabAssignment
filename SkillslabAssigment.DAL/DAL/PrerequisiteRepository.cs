using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace SkillslabAssigment.DAL.DAL
{
    public class PrerequisiteRepository : GenericRepository<Prerequisite, short>, IPrerequisiteRepository
    {
        public PrerequisiteRepository(DbConnection connection) : base(connection)
        {
        }
        public IEnumerable<Prerequisite> GetAllByTrainingId(short trainingId)
        {
            const string GET_ALL_BY_TRAINING_ID_Query = @"
                SELECT p.* FROM prerequisite p 
                INNER JOIN training_prerequisite tp 
                ON p.id = tp.prerequisite_id 
                WHERE tp.training_id = @TrainingId;
                ";
            return _connection.ExecuteQuery<Prerequisite>(GET_ALL_BY_TRAINING_ID_Query, new { TrainingId = trainingId });
        }
    }
}
