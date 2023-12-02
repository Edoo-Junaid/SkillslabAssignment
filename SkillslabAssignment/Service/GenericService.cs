using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Interface;
using SkillslabAssignment.Service;
using System.Collections.Generic;

namespace SkillslabAssignment.Interface
{
    public class GenericService<T> : IGenericService<T> where T : IEntity
    {
        protected readonly IGenericRepository<T> _repository;
        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }
        T IGenericService<T>.Add(T entity)
        {
            return _repository.Add(entity);
        }
        bool IGenericService<T>.Delete(int id)
        {
            return _repository.Delete(id);
        }
        IEnumerable<T> IGenericService<T>.GetAll()
        {
            return _repository.GetAll();
        }
        T IGenericService<T>.GetById(int id)
        {
            return _repository.GetById(id);
        }
        bool IGenericService<T>.Update(T entity)
        {
            return _repository.Update(entity);
        }
    }
}
