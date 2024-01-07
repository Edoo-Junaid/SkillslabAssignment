using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkillslabAssignment.Common.Enums;
namespace SkillslabAssigment.DAL.Interface
{
    public interface IEnrollmentRepository : IGenericRepository<Enrollement, int>
    {
        Task<IEnumerable<EnrollementDTO>> GetAllByManagerIdAsync(short managerId);
        Task<IEnumerable<SelectedUserDTO>> GetAllSelectedUsersAsync(short trainingId);
        Task<bool> UpdateEnrollmentStatus(int enrollmentId, EnrollementStatus status);
        Task<bool> EnrollmentExistsAsync(short trainingId);
        Task<bool> DeclineEnrollement(int enrollmentId, string declineReason);
        Task<IEnumerable<EnrollmentDetailsDto>> GetEnrollmentDetailsByUserIdAsync(short userId);
    }
}