using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Data;

namespace SkillslabAssigment.DAL.DAL
{
    public class AttachmentRepository : GenericRepository<Attachment, short>, IAttachmentRepository
    {
        public AttachmentRepository(IDbConnection connection) : base(connection)
        {
        }
        public IEnumerable<Attachment> GetAllByEnrollmentId(int enrollmentId)
        {
            return _connection.SelectWhere<Attachment>("enrollment_id = @EnrollmentId", new { EnrollmentId = enrollmentId });
        }
    }
}
