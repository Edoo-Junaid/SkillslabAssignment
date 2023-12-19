using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface IAttachmentService : IGenericService<Attachment, short>
    {
        Task<IEnumerable<AttachementDTO>> GetAllByEnrollmentIdAsync(int enrollmentId);
    }
}
