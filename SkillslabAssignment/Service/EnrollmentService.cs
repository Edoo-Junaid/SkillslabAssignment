using OfficeOpenXml;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Enums;
using SkillslabAssignment.Interface;
using SkillslabAssignment.Notification;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class EnrollmentService : GenericService<Enrollement, int>, IEnrollementService
    {
        private readonly IStorrageService _storrageService;
        private readonly IGenericRepository<Attachment, short> _attachmentRepository;
        private readonly IEnrollmentRepository _enrollementRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly INotificationManager _notificationManager;
        public EnrollmentService(
            IEnrollmentRepository repository,
            IStorrageService storrageService,
            IGenericRepository<Attachment, short> attachmentRepository,
            ITrainingRepository trainingRepository,
            INotificationManager notificationManager
            ) : base(repository)
        {
            _storrageService = storrageService;
            _attachmentRepository = attachmentRepository;
            _enrollementRepository = repository;
            _trainingRepository = trainingRepository;
            _notificationManager = notificationManager;
        }

        public async Task<IEnumerable<EnrollementDTO>> GetAllByManagerIdAsync(short managerId)
        {
            return await _enrollementRepository.GetAllByManagerIdAsync(managerId);
        }
        public async Task RunAutomaticProcessing()
        {
            IEnumerable<Training> trainings = await _trainingRepository.GetAllByRegistrationDeadline(System.DateTime.Today);
            foreach (var training in trainings)
            {
                await ProcessTrainingAsync(training);
            }
        }
        public async Task<bool> ProcessEnrollementAsync(EnrollementRequestDTO enrollementRequest)
        {
            if (enrollementRequest is null) return false;

            Enrollement enrollement = await CreateEnrollment(enrollementRequest);
            await UploadAndAddAttachments(enrollement, enrollementRequest.PrerequisiteToAttachment, enrollementRequest.ContentType);
            return true;
        }
        public async Task<EnrollementRequestDTO> ProcessMultipartContentAsync(MultipartMemoryStreamProvider provider)
        {
            EnrollementRequestDTO enrollementRequest = new EnrollementRequestDTO()
            {
                PrerequisiteToAttachment = new Dictionary<short, Stream>()
            };
            foreach (var file in provider.Contents)
            {
                if (file.Headers.ContentType != null)
                {
                    var prerequisiteId = short.Parse(file.Headers.ContentDisposition.Name.Trim('\"'));
                    var fileStream = await file.ReadAsStreamAsync();
                    enrollementRequest.PrerequisiteToAttachment.Add(prerequisiteId, fileStream);
                    enrollementRequest.ContentType = file.Headers.ContentType.MediaType;
                }
                else
                {
                    var fieldName = file.Headers.ContentDisposition.Name.Trim('\"');
                    var fieldValue = await file.ReadAsStringAsync();
                    switch (fieldName)
                    {
                        case "TrainingId":
                            enrollementRequest.TrainingId = short.Parse(fieldValue);
                            break;
                        case "UserId":
                            enrollementRequest.UserId = short.Parse(fieldValue);
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
        private async Task<Enrollement> CreateEnrollment(EnrollementRequestDTO enrollementRequest)
        {
            return await _repository.AddAsync(new Enrollement
            {
                TrainingId = enrollementRequest.TrainingId,
                UserId = enrollementRequest.UserId,
                Date = System.DateTime.Now,
                Status = EnrollementStatus.Pending.ToString(),
            });
        }
        private async Task UploadAndAddAttachments(Enrollement enrollement, Dictionary<short, Stream> prerequisiteToAttachment, string contentType)
        {
            foreach (var attachement in prerequisiteToAttachment)
            {
                var fileId = Guid.NewGuid();
                await _storrageService.UploadFileAsync(attachement.Value, enrollement.TrainingId, fileId, contentType);
                await _attachmentRepository.AddAsync(new Attachment
                {
                    EnrollmentId = enrollement.Id,
                    PrerequisiteId = attachement.Key,
                    FileId = fileId
                });
            }
        }
        public async Task<bool> ApproveEnrollementAsync(int enrollmentId)
        {
            Enrollement enrollement = await _enrollementRepository.GetByIdAsync(enrollmentId);
            Training training = await _trainingRepository.GetByIdAsync(enrollement.TrainingId);
            string trainingName = training.Name;
            string htmlBody = GenerateApprovalBody(enrollmentId, trainingName);
            await _notificationManager.NotifyHandlers("Enrollment approved", htmlBody, enrollement.UserId);
            return await _enrollementRepository.UpdateEnrollmentStatus(enrollmentId, EnrollementStatus.Approved);
        }
        public async Task<bool> DeclineEnrollementAsync(DeclineEnrollmentRequestDTO declineEnrollmentRequest)
        {
            Enrollement enrollement = await _enrollementRepository.GetByIdAsync(declineEnrollmentRequest.EnrollmentId);
            Training training = await _trainingRepository.GetByIdAsync(enrollement.TrainingId);
            string trainingName = training.Name;
            string htmlBody = GenerateDeclineBody(declineEnrollmentRequest.EnrollmentId, trainingName, declineEnrollmentRequest.DeclineReason);
            await _notificationManager.NotifyHandlers("Enrollment Declined", htmlBody, enrollement.UserId);
            return await _enrollementRepository.DeclineEnrollement(declineEnrollmentRequest.EnrollmentId, declineEnrollmentRequest.DeclineReason);
        }
        private async Task ProcessTrainingAsync(Training training)
        {
            string htmlBody = GenerateSelectedUserBody(training.Name);
            IEnumerable<SelectedUserDTO> selectedUsers = await _enrollementRepository.SelectUsersAsync(training.Id);
            foreach (var selectedUser in selectedUsers)
            {
                await _notificationManager.NotifyHandlers("Enrollment Successful", htmlBody, selectedUser.UserId);
            }
        }
        private string GenerateApprovalBody(int enrollmentId, string trainingName)
        {
            StringBuilder plainTextContent = new StringBuilder();
            plainTextContent.AppendLine($"Enrollment Approved for {trainingName}.");
            plainTextContent.AppendLine();
            plainTextContent.AppendLine($"Your enrollment with ID {enrollmentId} for the training '{trainingName}' has been approved.");
            return plainTextContent.ToString();
        }
        private string GenerateDeclineBody(int enrollmentId, string trainingName, string declineReason)
        {
            StringBuilder plainTextContent = new StringBuilder();
            plainTextContent.AppendLine($"Enrollment Declined for {trainingName}");
            plainTextContent.AppendLine();
            plainTextContent.AppendLine($"Your enrollment with ID {enrollmentId} for the training '{trainingName}' has been declined.");
            plainTextContent.AppendLine($"Reason for Decline: {declineReason}");
            return plainTextContent.ToString();
        }

        private string GenerateSelectedUserBody(string trainingName)
        {
            StringBuilder plainTextContent = new StringBuilder();
            plainTextContent.AppendLine($"Enrollment Successful for {trainingName}");
            plainTextContent.AppendLine();
            plainTextContent.AppendLine($"Congratulations! You have successfully been enrolled in the training '{trainingName}' and have been Selected.");
            return plainTextContent.ToString();
        }

        public async Task<IEnumerable<EnrollmentDetailsDto>> GetEnrollmentDetailsByUserIdAsync(short userId)
        {
            return await _enrollementRepository.GetEnrollmentDetailsByUserIdAsync(userId);
        }

        public async Task<MemoryStream> ExportSelectedUsers(short trainingId)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            IEnumerable<SelectedUserDTO> selectedUsers = await _enrollementRepository.GetAllUsersByTrainingIdAndEnrollmentStatus(trainingId, EnrollementStatus.Selected);
            using (var package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells["A1"].Value = "User Id";
                worksheet.Cells["B1"].Value = "Name";
                worksheet.Cells["C1"].Value = "Email";
                worksheet.Cells["D1"].Value = "Manager Name";
                int row = 2;
                foreach (var user in selectedUsers)
                {
                    worksheet.Cells[row, 1].Value = user.UserId;
                    worksheet.Cells[row, 2].Value = user.Name;
                    worksheet.Cells[row, 3].Value = user.Email;
                    worksheet.Cells[row, 4].Value = user.ManagerName;
                    row++;
                }
                MemoryStream stream = new MemoryStream(await package.GetAsByteArrayAsync());
                return stream;
            }
        }

    }
}
