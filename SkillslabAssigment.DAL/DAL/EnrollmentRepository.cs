using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace SkillslabAssigment.DAL.DAL
{
    public class EnrollmentRepository : GenericRepository<Enrollement, int>, IEnrollmentRepository
    {
        public EnrollmentRepository(DbConnection connection) : base(connection)
        {
        }
        public IEnumerable<EnrollementDTO> GetAllByManagerId(short managerId)
        {
            const string GET_ALL_ENROLLMENTS_BY_MANAGER_ID = @"
            SELECT [user].first_name,[user].last_name ,training.name AS training_name,enrollment.id AS enrollement_id 
            FROM [user] 
            INNER JOIN enrollment on
            [user].id = [enrollment].user_id
            INNER JOIN training on
            training.id = enrollment.training_id
            WHERE enrollment.status='pending' AND manager_id = @ManagerId";
            return _connection.ExecuteQuery<EnrollementDTO>(GET_ALL_ENROLLMENTS_BY_MANAGER_ID, new { ManagerId = managerId });
        }

    }
}
