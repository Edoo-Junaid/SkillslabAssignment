using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;

namespace SkillslabAssignment.Service
{
    public class DepartmentService : GenericService<Department, byte>, IDepartmentService
    {
        public DepartmentService(IGenericRepository<Department, byte> repository) : base(repository)
        {
        }
    }
}
