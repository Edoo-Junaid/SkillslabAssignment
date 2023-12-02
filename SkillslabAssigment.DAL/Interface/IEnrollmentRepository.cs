using SkillslabAssignment.Common.Entities;
using System.Collections.Generic;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IEnrollmentRepository : IGenericRepository<Enrollement>
    {
        IEnumerable<Enrollement> GetAllByManagerId(int managerId);
    }
}