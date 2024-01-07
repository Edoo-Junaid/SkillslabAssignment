using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class AttachmentService : GenericService<Attachment, short>, IAttachmentService
    {
        public IAttachmentRepository _attachmentRepository;
        public IPrerequisiteRepository _prerequisiteRepository;
        public IStorrageService _storrageService;
        public AttachmentService(IAttachmentRepository attachmentRepository, IPrerequisiteRepository prerequisiteRepository, IStorrageService storrageService) : base(attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
            _prerequisiteRepository = prerequisiteRepository;
            _storrageService = storrageService;
        }
        public async Task<IEnumerable<AttachementDTO>> GetAllByEnrollmentIdAsync(int enrollmentId)
        {
            IEnumerable<Attachment> attachments = _attachmentRepository.GetAllByEnrollmentIdAsync(enrollmentId);
            List<AttachementDTO> attachementDTOs = new List<AttachementDTO>();
            foreach (Attachment attachment in attachments)
            {
                attachementDTOs.Add(new AttachementDTO
                {
                    AttachmentId = attachment.Id,
                    EnrollmentId = attachment.EnrollmentId,
                    Prerequisite = await _prerequisiteRepository.GetByIdAsync(attachment.PrerequisiteId),
                    Url = await _storrageService.GetSignedUrlByObjectNameAsync(attachment.FileId.ToString())
                });
            }
            return attachementDTOs;
        }
    }
}
