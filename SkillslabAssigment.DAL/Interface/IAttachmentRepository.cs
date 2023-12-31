using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IAttachmentRepository : IGenericRepository<Attachment, short>
    {
        IEnumerable<Attachment> GetAllByEnrollmentIdAsync(int enrollmentId);
    }
}
