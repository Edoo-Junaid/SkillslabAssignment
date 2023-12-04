using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Mapper;
using SkillslabAssignment.Interface;
using System.Collections.Generic;
using System.Linq;

namespace SkillslabAssignment.Service
{
    public class TrainingService : GenericService<Training>, ITrainingService
    {
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IPrerequisiteRepository _prerequisiteRepository;
        public TrainingService(IGenericRepository<Training> repository, IPrerequisiteRepository prerequisiteRepository, IGenericRepository<Department> departmentRepository) : base(repository)
        {
            _prerequisiteRepository = prerequisiteRepository;
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<TrainingDTO> GetAllTrainingDTO()
        {
            return _repository.GetAll().Select(training => training.ToDTO());
        }

        public TrainingDetailsDTO GetTrainingDetails(int id)
        {
            Training training = _repository.GetById(id);
            Department department = _departmentRepository.GetById(training.DepartmentId);
            IEnumerable<Prerequisite> prerequisites = _prerequisiteRepository.GetAllByTrainingId(id);
            return training.ToDetailsDTO(department, prerequisites);
        }
    }
}
