using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class EnrollmentService : GenericService<Enrollement>, IEnrollementService
    {
        public IStorrageService _storrageService;
        public IGenericRepository<Attachment> _attachmentRepository;
        public IEnrollmentRepository _enrollementRepository;
        public EnrollmentService(IEnrollmentRepository repository, IStorrageService storrageService, IGenericRepository<Attachment> attachmentRepository) : base(repository)
        {
            _storrageService = storrageService;
            _attachmentRepository = attachmentRepository;
            _enrollementRepository = repository;
        }
        public IEnumerable<EnrollementDTO> GetAllByManagerId(int managerId)
        {
            return _enrollementRepository.GetAllByManagerId(managerId);
        }
        public async Task<bool> ProcessEnrollement(EnrollementRequestDTO enrollementRequest)
        {
            if (enrollementRequest == null)
            {
                return false;
            }
            Enrollement enrollement = CreateEnrollment(enrollementRequest);
            await UploadAndAddAttachments(enrollement, enrollementRequest.PrerequisiteToAttachment);
            return true;
        }
        public async Task<EnrollementRequestDTO> ProcessMultipartContent(MultipartMemoryStreamProvider provider)
        {
            EnrollementRequestDTO enrollementRequest = new EnrollementRequestDTO()
            {
                PrerequisiteToAttachment = new Dictionary<int, Stream>()
            };
            List<Stream> attachments = new List<Stream>();
            foreach (var file in provider.Contents)
            {
                if (file.Headers.ContentType != null)
                {
                    var prerequisiteId = int.Parse(file.Headers.ContentDisposition.Name.Trim('\"'));
                    var fileStream = await file.ReadAsStreamAsync();
                    enrollementRequest.PrerequisiteToAttachment.Add(prerequisiteId, fileStream);
                }
                else
                {
                    var fieldName = file.Headers.ContentDisposition.Name.Trim('\"');
                    var fieldValue = await file.ReadAsStringAsync();
                    switch (fieldName)
                    {
                        case "TrainingId":
                            enrollementRequest.TrainingId = int.Parse(fieldValue);
                            break;
                        case "UserId":
                            enrollementRequest.UserId = int.Parse(fieldValue);
                            break;
                    }
                }
            }
            if (enrollementRequest.TrainingId == 0 || enrollementRequest.UserId == 0)
            {
                return null;
            }
            return enrollementRequest;
        }
        private Enrollement CreateEnrollment(EnrollementRequestDTO enrollementRequest)
        {
            return _repository.Add(new Enrollement
            {
                TrainingId = enrollementRequest.TrainingId,
                UserId = enrollementRequest.UserId,
                Date = System.DateTime.Now,
                Status = EnrollementStatus.Pending.ToString()
            });
        }
        private async Task UploadAndAddAttachments(Enrollement enrollement, Dictionary<int, Stream> prerequisiteToAttachment)
        {
            foreach (var attachement in prerequisiteToAttachment)
            {
                string fileName = $"user_{enrollement.UserId}_file{attachement.Key}";
                string url = await _storrageService.UploadFileAsync(attachement.Value, enrollement.TrainingId, fileName);
                _attachmentRepository.Add(new Attachment
                {
                    EnrollmentId = enrollement.Id,
                    PrerequisiteId = attachement.Key,
                    Url = url
                });
            }
        }
    }
}
