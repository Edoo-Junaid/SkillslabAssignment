using SkillslabAssignment.Common.Interface;
using System.Collections.Generic;

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
