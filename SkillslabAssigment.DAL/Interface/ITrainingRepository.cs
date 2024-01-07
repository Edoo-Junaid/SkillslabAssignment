using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public interface ITrainingRepository : IGenericRepository<Training, short>
    {
        Task<IEnumerable<Training>> GetAllEnrolledTrainingAsync(short userId);
        Task<IEnumerable<Training>> GetAllWithSearchAsync(string columnName, string searchValue);
        Task<bool> CreateTrainingAsync(CreateTrainingRequestDTO training);
        Task<IEnumerable<Training>> GetAllByRegistrationDeadline(DateTime deadline);
        Task<bool> UpdateTraninigAndPrerequisiteAsync(TrainingDetailsDTO training);
        Task<bool> DeleteTrainingAndPrerequisiteAsync(short trainingId);


    }
}
