using SkillslabAssignment.Common.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssignment.Service
{
    public interface IGenericService<T, TId> where T : IEntity<TId>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(TId id);
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(TId id);
    }
}
