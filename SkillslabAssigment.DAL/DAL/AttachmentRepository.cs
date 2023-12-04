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
    public class AttachmentRepository : GenericRepository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(IDbConnection connection) : base(connection)
        {
        }

        public IEnumerable<Attachment> GetAllByEnrollmentId(int enrollmentId)
        {
            return _connection.ExecuteQuery<Attachment>("enrollment_id = @EnrollmentId", new { EnrollmentId = enrollmentId });
        }
    }
}
