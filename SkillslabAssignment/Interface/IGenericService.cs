using SkillslabAssignment.Common.Interface;
using System.Collections.Generic;

namespace SkillslabAssignment.Service
{
    public interface IGenericService<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Add(T entity);
        bool Update(T entity);
        bool Delete(int id);
    }
}
