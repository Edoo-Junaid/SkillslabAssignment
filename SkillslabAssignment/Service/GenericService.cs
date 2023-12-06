using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Interface;
using SkillslabAssignment.Service;
using System.Collections.Generic;

namespace SkillslabAssignment.Interface
{
    public class GenericService<T> : IGenericService<T> where T : IEntity
    {
        protected readonly IGenericRepository<T> _repository;
        public GenericService(IGenericRepository<T> repository) => _repository = repository;
        T IGenericService<T>.Add(T entity) => _repository.Add(entity);
        bool IGenericService<T>.Delete(int id) => _repository.Delete(id);
        IEnumerable<T> IGenericService<T>.GetAll() => _repository.GetAll();
        T IGenericService<T>.GetById(int id) => _repository.GetById(id);
        bool IGenericService<T>.Update(T entity) => _repository.Update(entity);
    }
}
