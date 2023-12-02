using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;

namespace SkillslabAssignment.Service
{
    public class EnrollmentService : GenericService<Enrollement>, IEnrollementService
    {
        public EnrollmentService(IEnrollmentRepository repository) : base(repository)
        {
        }
    }
}
