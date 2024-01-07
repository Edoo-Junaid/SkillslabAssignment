using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Interface;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.DAL
{
    public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : IEntity<TId>
    {
        protected DbConnection _connection;
        public GenericRepository(DbConnection connection) => _connection = connection;
        public async Task<bool> DeleteAsync(TId id)
        {
            //TODO
            await _connection.DeleteByIdAsync<T>(id);
            return true;
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await _connection.GetAllAsync<T>();
        public async Task<T> GetByIdAsync(TId id) => await _connection.GetByIdAsync<T>(id);
        public async Task<T> AddAsync(T entity) => await _connection.ExecuteInsertQueryAsync<T>(entity);
        public async Task<bool> UpdateAsync(T entity)
        {
            //TODO
            await _connection.UpdateByIdAsync<T>(entity.Id, entity);
            return true;
        }
        public async Task<IEnumerable<T>> GetPaginatedDataAsync(int pageSize, int pageNumber)
        {
            return await _connection.GetPaginatedDataAsync<T>(pageSize, pageNumber);
        }
        public async Task<int> GetTotalRowCountAsync()
        {
            return await _connection.GetRowCountAsync<T>();
        }
    }
}
