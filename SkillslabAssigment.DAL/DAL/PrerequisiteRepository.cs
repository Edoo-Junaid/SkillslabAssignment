using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.DAL
{
    public class PrerequisiteRepository : GenericRepository<Prerequisite, short>, IPrerequisiteRepository
    {
        public PrerequisiteRepository(DbConnection connection) : base(connection)
        {
        }
        public async Task<IEnumerable<Prerequisite>> GetAllByTrainingIdAsync(short trainingId)
        {
            const string GET_ALL_BY_TRAINING_ID_Query = @"
                SELECT p.* FROM prerequisite p 
                INNER JOIN training_prerequisite tp 
                ON p.id = tp.prerequisite_id 
                WHERE tp.training_id = @TrainingId;
                ";
            return await _connection.ExecuteQueryAsync<Prerequisite>(GET_ALL_BY_TRAINING_ID_Query, new { TrainingId = trainingId });
        }
    }
}
