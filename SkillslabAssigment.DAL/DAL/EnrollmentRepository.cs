using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.DAL
{
    public class EnrollmentRepository : GenericRepository<Enrollement, int>, IEnrollmentRepository
    {
        public EnrollmentRepository(DbConnection connection) : base(connection)
        {
        }

        public async Task<bool> EnrollmentExistsAsync(short trainingId)
        {
            return await _connection.RowExistsAsync<Enrollement>("training_id = @TrainingId", new { TrainingId = trainingId });
        }

        public async Task<IEnumerable<EnrollementDTO>> GetAllByManagerIdAsync(short managerId)
        {
            const string GET_ALL_ENROLLMENTS_BY_MANAGER_ID = @"
                SELECT [user].first_name,[user].last_name ,training.name AS training_name,enrollment.id AS enrollement_id 
                FROM [user] 
                INNER JOIN enrollment on [user].id = [enrollment].user_id
                INNER JOIN training on training.id = enrollment.training_id
                WHERE enrollment.status='pending' AND manager_id = @ManagerId
            ";
            return await _connection.ExecuteQueryAsync<EnrollementDTO>(GET_ALL_ENROLLMENTS_BY_MANAGER_ID, new { ManagerId = managerId });
        }

        public async Task<IEnumerable<SelectedUserDTO>> GetAllSelectedUsersAsync(short trainingId)
        {
            const string GET_ALL_SELECTED_USERS = @"
                DECLARE @SelectedRows TABLE (
                    user_id INT,
                    email NVARCHAR(MAX)
                );
                WITH SelectedRows AS (
                    SELECT TOP (SELECT training_capacity FROM training WHERE id = @TrainingId)
                    u.id AS user_id, a.email,u.department_id
                    FROM [user] u
                    JOIN [account] a ON u.account_id = a.id
                    JOIN [enrollment] e ON e.user_id = u.id
                    JOIN training t ON e.training_id = t.id
                    WHERE t.id = @TrainingId AND e.[status]='Approved'
	                ORDER BY
	                  IIF(u.department_id = t.department_id, 0, 1),
	                  e.date
                )
                UPDATE e
                SET status = 'Selected'
                OUTPUT inserted.user_id, sr.email INTO @SelectedRows
                FROM enrollment e
                JOIN SelectedRows sr ON e.user_id = sr.user_id;
                SELECT user_id, email
                FROM @SelectedRows;
            ";
            return await _connection.ExecuteQueryAsync<SelectedUserDTO>(GET_ALL_SELECTED_USERS, new { TrainingId = trainingId });
        }

        public async Task<bool> UpdateEnrollmentStatus(int enrollmentId, EnrollementStatus status)
        {
            return await _connection.UpdateByIdAsync<Enrollement>(enrollmentId, new { status = status.ToString() });
        }

        public async Task<bool> DeclineEnrollement(int enrollmentId, string declineReason)
        {
            return await _connection.UpdateByIdAsync<Enrollement>(enrollmentId, new { status = EnrollementStatus.Declined.ToString(), decline_reason = declineReason });
        }

        public async Task<IEnumerable<EnrollmentDetailsDto>> GetEnrollmentDetailsByUserIdAsync(short userId)
        {
            const string GET_ENROLLMENT_DETAILS_BY_USER_ID = @"
                SELECT t.id AS training_id, t.name AS training_name, t.date AS training_start_date, e.status AS enrollment_status
                FROM enrollment e
                JOIN training t ON e.training_id = t.id
                WHERE e.user_id = @UserId
            ";
            return await _connection.ExecuteQueryAsync<EnrollmentDetailsDto>(GET_ENROLLMENT_DETAILS_BY_USER_ID, new { UserId = userId });
        }
    }
}
