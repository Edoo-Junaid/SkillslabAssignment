using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;

namespace SkillslabAssignment.Service
{
    public class DepartmentService : GenericService<Department>, IDepartmentService
    {
        public DepartmentService(IGenericRepository<Department> repository) : base(repository)
        {
        }
    }
}
