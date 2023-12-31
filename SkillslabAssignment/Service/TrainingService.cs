using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Mapper;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace SkillslabAssignment.Service
{
    public class TrainingService : GenericService<Training, short>, ITrainingService
    {
        private readonly IGenericRepository<Department, byte> _departmentRepository;
        private readonly IPrerequisiteRepository _prerequisiteRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        public TrainingService(ITrainingRepository trainingRepository,
            IPrerequisiteRepository prerequisiteRepository,
            IGenericRepository<Department, byte> departmentRepository,
            IEnrollmentRepository enrollmentRepository) : base(trainingRepository)
        {
            _prerequisiteRepository = prerequisiteRepository;
            _departmentRepository = departmentRepository;
            _trainingRepository = trainingRepository;
            _enrollmentRepository = enrollmentRepository;
        }
        public async Task<IEnumerable<Training>> GetAllEnrolledTrainingAsync(short userId) => await _trainingRepository.GetAllEnrolledTrainingAsync(userId);
        public async Task<IEnumerable<TrainingDTO>> GetAllTrainingDTOAsync()
        {
            return (await _repository
                .GetAllAsync())
                .Select(training => training.ToDTO());
        }
        public async Task<TrainingDetailsDTO> GetTrainingDetailsAsync(short id)
        {
            Training training = await _trainingRepository.GetByIdAsync(id);
            Department department = await _departmentRepository.GetByIdAsync(training.DepartmentId);
            IEnumerable<Prerequisite> prerequisites = await _prerequisiteRepository.GetAllByTrainingIdAsync(id);
            return training.ToDetailsDTO(department, prerequisites);
        }
        public async Task<bool> CreteTrainingAsync(CreateTrainingRequestDTO training)
        {
            return await _trainingRepository.CreateTrainingAsync(training);
        }

        public async Task<bool> UpdateTrainingAndPrerequisiteAsync(TrainingDetailsDTO training)
        {
            return await _trainingRepository.UpdateTraninigAndPrerequisiteAsync(training);
        }

        public async Task<bool> DeleteTrainingAndPrerequisiteAsync(short trainingId)
        {
            if (await _enrollmentRepository.EnrollmentExistsAsync(trainingId))
            {
                return false;
            }
            return await _trainingRepository.DeleteTrainingAndPrerequisiteAsync(trainingId);
        }
    }
}
