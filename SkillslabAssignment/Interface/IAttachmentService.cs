using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Collections.Generic;

namespace SkillslabAssignment.Interface
{
    public interface IAttachmentService : IGenericService<Attachment>
    {
        IEnumerable<AttachementDTO> GetAllByEnrollmentId(int enrollmentId);
    }
}
