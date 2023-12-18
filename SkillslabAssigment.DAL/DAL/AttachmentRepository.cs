using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace SkillslabAssigment.DAL.DAL
{
    public class AttachmentRepository : GenericRepository<Attachment, short>, IAttachmentRepository
    {
        public AttachmentRepository(DbConnection connection) : base(connection)
        {
        }
        public IEnumerable<Attachment> GetAllByEnrollmentId(int enrollmentId)
        {
            return _connection.SelectWhere<Attachment>("enrollment_id = @EnrollmentId", new { EnrollmentId = enrollmentId });
        }
    }
}
