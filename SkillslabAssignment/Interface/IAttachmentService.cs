using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public interface IAttachmentService : IGenericService<Attachment>
    {
        IEnumerable<AttachementDTO> GetAllByEnrollmentId(int enrollmentId);
    }
}
