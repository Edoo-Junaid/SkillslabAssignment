using SkillslabAssignment.Common.Interface;
using System.Collections.Generic;

namespace SkillslabAssignment.Service
{
    public interface IGenericService<T, TId> where T : IEntity<TId>
    {
        IEnumerable<T> GetAll();
        T GetById(TId id);
        T Add(T entity);
        bool Update(T entity);
        bool Delete(TId id);
    }
}
