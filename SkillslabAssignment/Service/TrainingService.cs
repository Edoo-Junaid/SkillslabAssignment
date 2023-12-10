﻿using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Mapper;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.Linq;

namespace SkillslabAssignment.Service
{
    public class TrainingService : GenericService<Training, short>, ITrainingService
    {
        private readonly IGenericRepository<Department, byte> _departmentRepository;
        private readonly IPrerequisiteRepository _prerequisiteRepository;
        private readonly ITrainingRepository _trainingRepository;
        public TrainingService(ITrainingRepository trainingRepository,
            IPrerequisiteRepository prerequisiteRepository,
            IGenericRepository<Department, byte> departmentRepository) : base(trainingRepository)
        {
            _prerequisiteRepository = prerequisiteRepository;
            _departmentRepository = departmentRepository;
            _trainingRepository = trainingRepository;
        }
        public IEnumerable<Training> GetAllEnrolledTraining(short userId) => _trainingRepository.GetAllEnrolledTraining(userId);
        public IEnumerable<TrainingDTO> GetAllTrainingDTO()
        {
            return _repository.GetAll().Select(training => training.ToDTO());
        }
        public TrainingDetailsDTO GetTrainingDetails(short id)
        {
            Training training = _trainingRepository.GetById(id);
            Department department = _departmentRepository.GetById(training.DepartmentId);
            IEnumerable<Prerequisite> prerequisites = _prerequisiteRepository.GetAllByTrainingId(id);
            return training.ToDetailsDTO(department, prerequisites);
        }
        public bool CreteTraining(CreateTrainingRequestDTO training)
        {
            return _trainingRepository.CreateTraining(training);
        }
    }
}
