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
    public class TrainingRepository : GenericRepository<Training>, ITrainingRepository
    {
        public TrainingRepository(IDbConnection connection) : base(connection)
        {
        }

        public IEnumerable<Training> GetAllEnrolledTraining(int userId)
        {
            const string GET_ALL_ENROLLED_TRAINING_ID_QUERY = @"
                SELECT training.* FROM enrollment
                INNER JOIN training 
                ON training.id = enrollment.training_id
                WHERE user_id = @UserId"
            ;
            return _connection.ExecuteQuery<Training>(GET_ALL_ENROLLED_TRAINING_ID_QUERY, new { UserId = userId });
        }
    }
}
