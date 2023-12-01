using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public class EnrollmentService : GenericService<Enrollement>, IEnrollementService
    {
        public EnrollmentService(IEnrollmentRepository repository) : base(repository)
        {
        }
    }
}
