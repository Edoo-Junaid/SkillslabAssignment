using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : IEntity
    {
        protected IDbConnection _connection;
        public GenericRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public bool Delete(int id)
        {
            //TODO
            _connection.DeleteById<T>(id);
            return true;
        }
        public IEnumerable<T> GetAll()
        {
            return _connection.GetAll<T>();
        }
        public T GetById(int id)
        {
            return _connection.GetById<T>(id);
        }
        public T Add(T entity)
        {
            return _connection.ExecuteInsertQuery(entity);
        }
        public bool Update(T entity)
        {
            //TODO
            _connection.UpdateById<T>(entity.Id, entity);
            return true;
        }
    }
}
