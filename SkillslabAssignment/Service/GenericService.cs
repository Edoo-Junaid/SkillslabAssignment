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
        public async Task<IEnumerable<T>> GetPaginatedDataAsync(int pageSize, int pageNumber)
        {
            return await _repository.GetPaginatedDataAsync(pageSize, pageNumber);
        }

        public async Task<int> GetTotalRowCountAsync()
        {
            return await _repository.GetTotalRowCountAsync();
        }

        async Task<T> IGenericService<T, TId>.AddAsync(T entity) => await _repository.AddAsync(entity);
        async Task<bool> IGenericService<T, TId>.DeleteAsync(TId id) => await _repository.DeleteAsync(id);
        async Task<IEnumerable<T>> IGenericService<T, TId>.GetAllAsync() => await _repository.GetAllAsync();
        async Task<T> IGenericService<T, TId>.GetByIdAsync(TId id) => await _repository.GetByIdAsync(id);
        async Task<bool> IGenericService<T, TId>.UpdateAsync(T entity) => await _repository.UpdateAsync(entity);

    }
}
