using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System.Collections.Generic;

namespace SkillslabAssignment.Service
{
    public class AttachmentService : GenericService<Attachment, short>, IAttachmentService
    {
        public IAttachmentRepository _attachmentRepository;
        public IPrerequisiteRepository _prerequisiteRepository;
        public AttachmentService(IAttachmentRepository attachmentRepository, IPrerequisiteRepository prerequisiteRepository) : base(attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
            _prerequisiteRepository = prerequisiteRepository;
        }
        public IEnumerable<AttachementDTO> GetAllByEnrollmentId(int enrollmentId)
        {
            IEnumerable<Attachment> attachments = _attachmentRepository.GetAllByEnrollmentId(enrollmentId);
            List<AttachementDTO> attachementDTOs = new List<AttachementDTO>();
            foreach (Attachment attachment in attachments)
            {
                attachementDTOs.Add(new AttachementDTO
                {
                    AttachmentId = attachment.Id,
                    EnrollmentId = attachment.EnrollmentId,
                    Prerequisite = _prerequisiteRepository.GetById(attachment.PrerequisiteId),
                    Url = attachment.Url
                });
            }
            return attachementDTOs;
        }
    }
}
