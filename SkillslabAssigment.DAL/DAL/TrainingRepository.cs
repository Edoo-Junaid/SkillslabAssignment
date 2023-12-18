using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace SkillslabAssigment.DAL.DAL
{
    public class TrainingRepository : GenericRepository<Training, short>, ITrainingRepository
    {
        public TrainingRepository(DbConnection connection) : base(connection)
        {
        }
        public IEnumerable<Training> GetAllEnrolledTraining(short userId)
        {
            const string GET_ALL_ENROLLED_TRAINING_ID_QUERY = @"
                SELECT training.* FROM enrollment
                INNER JOIN training 
                ON training.id = enrollment.training_id
                WHERE user_id = @UserId"
            ;
            return _connection.ExecuteQuery<Training>(GET_ALL_ENROLLED_TRAINING_ID_QUERY, new { UserId = userId });
        }
        public bool CreateTraining(CreateTrainingRequestDTO training)
        {
            const string CREATE_TRAINING_QUERY = @"
                DECLARE @TrainingId INT;
                INSERT INTO training (name, description, date, location, department_id, deadline)
                VALUES (@Name, @Description, @Date, @Location, @DepartmentId, @Deadline)
                SET @TrainingId = SCOPE_IDENTITY(); 
                IF (@PrerequisitesId IS NOT NULL AND LEN(@PrerequisitesId) > 0)
                BEGIN
                    INSERT INTO training_prerequisite (training_id, prerequisite_id)
                    SELECT @TrainingId, P.value
                    FROM STRING_SPLIT(@PrerequisitesId, ',') AS P;
                END
            ";
            return _connection.ExecuteTransaction(CREATE_TRAINING_QUERY, new
            {
                training.Name,
                training.Description,
                training.Date,
                training.Location,
                training.DepartmentId,
                training.Deadline,
                PrerequisitesId = training.PrerequisitesId != null ? string.Join(",", training.PrerequisitesId) : null
            });
        }
    }
}
