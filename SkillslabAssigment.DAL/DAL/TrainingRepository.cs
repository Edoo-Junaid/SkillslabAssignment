using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.DAL
{
    public class TrainingRepository : GenericRepository<Training, short>, ITrainingRepository
    {
        public TrainingRepository(DbConnection connection) : base(connection)
        {
        }
        public async Task<IEnumerable<Training>> GetAllEnrolledTrainingAsync(short userId)
        {
            const string GET_ALL_ENROLLED_TRAINING_ID_QUERY = @"
                SELECT training.* FROM enrollment
                INNER JOIN training 
                ON training.id = enrollment.training_id
                WHERE user_id = @UserId"
            ;
            return await _connection.ExecuteQueryAsync<Training>(GET_ALL_ENROLLED_TRAINING_ID_QUERY, new { UserId = userId });
        }
        public async Task<bool> CreateTrainingAsync(CreateTrainingRequestDTO training)
        {
            const string CREATE_TRAINING_QUERY = @"
                DECLARE @TrainingId INT;
                INSERT INTO training (name, description, date, location, department_id, deadline,training_capacity)
                VALUES (@Name, @Description, @Date, @Location, @DepartmentId, @Deadline,@TrainingCapacity)
                SET @TrainingId = SCOPE_IDENTITY(); 
                IF (@PrerequisitesId IS NOT NULL AND LEN(@PrerequisitesId) > 0)
                BEGIN
                    INSERT INTO training_prerequisite (training_id, prerequisite_id)
                    SELECT @TrainingId, P.value
                    FROM STRING_SPLIT(@PrerequisitesId, ',') AS P;
                END
            ";
            return await _connection.ExecuteTransactionAsync(CREATE_TRAINING_QUERY, new
            {
                training.Name,
                training.Description,
                training.Date,
                training.Location,
                training.DepartmentId,
                training.Deadline,
                training.TrainingCapacity,
                PrerequisitesId = training.PrerequisitesId != null ? string.Join(",", training.PrerequisitesId) : null
            });
        }
        public async Task<IEnumerable<Training>> GetAllByRegistrationDeadline(DateTime date)
        {
            return await _connection.SelectWhereAsync<Training>("deadline=@Deadline", new { Deadline = date });
        }

        public async Task<bool> UpdateTraninigAndPrerequisiteAsync(TrainingDetailsDTO training)
        {
            const string UPDATE_TRAINING_QUERY = @"
                UPDATE training
                SET name = @Name,
                    description = @Description,
                    date = @Date,
                    location = @Location,
                    department_id = @DepartmentId,
                    deadline = @Deadline,
                    training_capacity = @TrainingCapacity
                WHERE id = @Id;
                DELETE FROM training_prerequisite WHERE training_id = @Id;
                IF (@PrerequisitesId IS NOT NULL AND LEN(@PrerequisitesId) > 0)
                BEGIN
                    INSERT INTO training_prerequisite (training_id, prerequisite_id)
                    SELECT @Id, P.value
                    FROM STRING_SPLIT(@PrerequisitesId, ',') AS P;
                END
            ";
            var prerequisiteIds = training.Prerequisites?.Select(p => p.Id);
            return await _connection.ExecuteTransactionAsync(UPDATE_TRAINING_QUERY, new
            {
                training.Id,
                training.Name,
                training.Description,
                training.Date,
                training.Location,
                training.DepartmentId,
                training.Deadline,
                training.TrainingCapacity,
                PrerequisitesId = prerequisiteIds != null ? string.Join(",", prerequisiteIds) : null
            });
        }
        public async Task<bool> DeleteTrainingAndPrerequisiteAsync(short trainingId)
        {
            const string DELETE_TRAINING_QUERY = @"
                DELETE FROM training_prerequisite WHERE training_id = @TrainingId;
                DELETE FROM training WHERE id = @TrainingId;
            ";
            return await _connection.ExecuteTransactionAsync(DELETE_TRAINING_QUERY, new { TrainingId = trainingId });
        }

        public Task<IEnumerable<Training>> GetAllWithSearchAsync(string columnName, string searchValue)
        {
            return _connection.GetAllWithSearchAsync<Training>(columnName, searchValue);
        }
    }
}
