using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.DAL
{
    public class EnrollmentRepository : GenericRepository<Enrollement>, IEnrollmentRepository
    {
        public EnrollmentRepository(IDbConnection connection) : base(connection)
        {
        }
        public IEnumerable<Enrollement> GetAllByManagerId(int managerId)
        {
            //return _connection.ExecuteQuery<Enrollement>("SELECT * FROM Enrollement WHERE ManagerId = @ManagerId", new { ManagerId = managerId });
            throw new NotImplementedException();
            //todo: implement this method
        }
    }
}
