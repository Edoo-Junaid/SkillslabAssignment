using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Interface;
using SkillslabAssignment.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillslabAssignment.Interface
{
    public class GenericService<T, TId> : IGenericService<T, TId> where T : IEntity<TId>
    {
        protected readonly IGenericRepository<T, TId> _repository;
        public GenericService(IGenericRepository<T, TId> repository) => _repository = repository;
        Task<T> IGenericService<T, TId>.AddAsync(T entity) => _repository.AddAsync(entity);
        Task<bool> IGenericService<T, TId>.DeleteAsync(TId id) => _repository.DeleteAsync(id);
        Task<IEnumerable<T>> IGenericService<T, TId>.GetAllAsync() => _repository.GetAllAsync();
        Task<T> IGenericService<T, TId>.GetByIdAsync(TId id) => _repository.GetByIdAsync(id);
        Task<bool> IGenericService<T, TId>.UpdateAsync(T entity) => _repository.UpdateAsync(entity);
    }
}
