using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Interface;
using SkillslabAssignment.Service;
using System.Collections.Generic;

namespace SkillslabAssignment.Interface
{
    public class GenericService<T, TId> : IGenericService<T, TId> where T : IEntity<TId>
    {
        protected readonly IGenericRepository<T, TId> _repository;
        public GenericService(IGenericRepository<T, TId> repository) => _repository = repository;
        T IGenericService<T, TId>.Add(T entity) => _repository.Add(entity);
        bool IGenericService<T, TId>.Delete(TId id) => _repository.Delete(id);
        IEnumerable<T> IGenericService<T, TId>.GetAll() => _repository.GetAll();
        T IGenericService<T, TId>.GetById(TId id) => _repository.GetById(id);
        bool IGenericService<T, TId>.Update(T entity) => _repository.Update(entity);
    }
}
