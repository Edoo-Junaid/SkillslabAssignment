using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Interface;
using System.Collections.Generic;
using System.Data;

namespace SkillslabAssigment.DAL.DAL
{
    public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : IEntity<TId>
    {
        protected IDbConnection _connection;
        public GenericRepository(IDbConnection connection) => _connection = connection;
        public bool Delete(TId id)
        {
            //TODO
            _connection.DeleteById<T>(id);
            return true;
        }
        public IEnumerable<T> GetAll() => _connection.GetAll<T>();
        public T GetById(TId id) => _connection.GetById<T>(id);
        public T Add(T entity) => _connection.ExecuteInsertQuery<T>(entity);
        public bool Update(T entity)
        {
            //TODO
            _connection.UpdateById<T>(entity.Id, entity);
            return true;
        }
    }
}
