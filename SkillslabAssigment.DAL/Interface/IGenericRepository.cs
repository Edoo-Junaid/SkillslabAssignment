using SkillslabAssignment.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Interface
{
    public interface IGenericRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Add(T entity);
        bool Update(T entity);
        bool Delete(int id);
    }
}
