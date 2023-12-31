﻿using SkillslabAssigment.DAL.Interface;
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
    public class EnrollmentService : GenericService<Enrollement, int>, IEnrollementService
    {
        public IStorrageService _storrageService;
        public IGenericRepository<Attachment, short> _attachmentRepository;
        public IEnrollmentRepository _enrollementRepository;
        public ITrainingRepository _trainingRepository;
        public EnrollmentService(
            IEnrollmentRepository repository,
            IStorrageService storrageService,
            IGenericRepository<Attachment, short> attachmentRepository,
            ITrainingRepository trainingRepository
            ) : base(repository)
        {
            _storrageService = storrageService;
            _attachmentRepository = attachmentRepository;
            _enrollementRepository = repository;
            _trainingRepository = trainingRepository;
        }

        public async Task<IEnumerable<EnrollementDTO>> GetAllByManagerIdAsync(short managerId)
        {
            return await _enrollementRepository.GetAllByManagerIdAsync(managerId);
        }
        public async Task RunAutomaticProcessing()
        {
            IEnumerable<Training> trainings = await _trainingRepository.GetAllByRegistrationDeadline(new System.DateTime(2024, 02, 15));
            trainings.ToList().ForEach(async training =>
            {
                IEnumerable<SelectedUserDTO> selectedUsers = await _enrollementRepository.GetAllSelectedUsersAsync(training.Id);
            });
        }
        public async Task<bool> ProcessEnrollementAsync(EnrollementRequestDTO enrollementRequest)
        {
            if (enrollementRequest is null) return false;

            Enrollement enrollement = await CreateEnrollment(enrollementRequest);
            await UploadAndAddAttachments(enrollement, enrollementRequest.PrerequisiteToAttachment);
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
                Status = EnrollementStatus.Pending.ToString()
            });
        }
        private async Task UploadAndAddAttachments(Enrollement enrollement, Dictionary<short, Stream> prerequisiteToAttachment)
        {
            foreach (var attachement in prerequisiteToAttachment)
            {
                string fileName = $"user_{enrollement.UserId}_file{attachement.Key}";
                string url = await _storrageService.UploadFileAsync(attachement.Value, enrollement.TrainingId, fileName);
                await _attachmentRepository.AddAsync(new Attachment
                {
                    EnrollmentId = enrollement.Id,
                    PrerequisiteId = attachement.Key,
                    Url = url
                });
            }
        }
        public async Task<bool> ApproveEnrollementAsync(int enrollmentId)
        {
            return await _enrollementRepository.UpdateEnrollmentStatus(enrollmentId, EnrollementStatus.Approved);
        }

        public async Task<bool> DeclineEnrollementAsync(DeclineEnrollmentRequestDTO declineEnrollmentRequestDTO)
        {
            return await _enrollementRepository.DeclineEnrollement(declineEnrollmentRequestDTO.EnrollmentId, declineEnrollmentRequestDTO.DeclineReason);
        }
    }
}
