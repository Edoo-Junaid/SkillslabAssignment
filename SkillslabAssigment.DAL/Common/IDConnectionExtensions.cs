using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.Common
{
    public static class IDConnectionExtensions
    {
        public static async Task ExecuteNonQueryAsync(this DbConnection connection, string query, object parameters = null)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    await connection.OpenAsync();
                    command.CommandText = query;
                    AddParametersToCommand(command, parameters);
                    await command.ExecuteNonQueryAsync();
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public static bool ExecuteTransaction(this IDbConnection connection, string transactionQuery, object parameters = null)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    using (IDbTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            command.CommandText = transactionQuery;
                            command.Transaction = transaction;
                            AddParametersToCommand(command, parameters);
                            command.ExecuteNonQuery();
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Exception: {ex.Message}");
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public static async Task<bool> ExecuteTransactionAsync(this DbConnection connection, string transactionQuery, object parameters = null)
        {
            await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        command.CommandText = transactionQuery;
                        command.Transaction = transaction;
                        AddParametersToCommand(command, parameters);
                        await command.ExecuteNonQueryAsync();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception: {ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public static T ExecuteInsertQuery<T>(this IDbConnection connection, T parameters)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.OpenConnection();
                    Type type = parameters.GetType();
                    var insertQuery = CreateInsertQuery(type, parameters, command);
                    command.CommandText = insertQuery;
                    using (var reader = command.ExecuteReader())
                    {
                        return ReadResultSet<T>(type, reader).FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.CloseConnection();
                }
            }
        }
        public async static Task<T> ExecuteInsertQueryAsync<T>(this DbConnection connection, T parameters)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    await connection.OpenAsync();
                    Type type = parameters.GetType();
                    var insertQuery = CreateInsertQuery(type, parameters, command);
                    command.CommandText = insertQuery;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return ReadResultSet<T>(type, reader).FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public static IEnumerable<T> GetAll<T>(this IDbConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.OpenConnection();
                    Type type = typeof(T);
                    command.CommandText = $"SELECT * FROM [{GetTableName(type)}]";
                    using (var reader = command.ExecuteReader())
                    {
                        return ReadResultSet<T>(type, reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.CloseConnection();
                }
            }
        }
        public async static Task<IEnumerable<T>> GetAllAsync<T>(this DbConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    await connection.OpenAsync();
                    Type type = typeof(T);
                    command.CommandText = $"SELECT * FROM [{GetTableName(type)}]";
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return ReadResultSet<T>(type, reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public async static Task<int> GetRowCountAsync<T>(this DbConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    await connection.OpenAsync();
                    Type type = typeof(T);
                    command.CommandText = $"SELECT COUNT(*) FROM [{GetTableName(type)}]";
                    var rowCount = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(rowCount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async static Task<IEnumerable<T>> GetPaginatedDataAsync<T>(this DbConnection connection, int pageSize, int pageNumber, string orderBy = "id")
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    await connection.OpenAsync();
                    Type type = typeof(T);
                    int offset = (pageNumber - 1) * pageSize;
                    var queryBuilder = new StringBuilder($"SELECT * FROM [{GetTableName(type)}]");
                    queryBuilder.Append($" ORDER BY {orderBy}");
                    queryBuilder.Append($" OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY");
                    command.CommandText = queryBuilder.ToString();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return ReadResultSet<T>(type, reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public async static Task<IEnumerable<T>> GetAllWithSearchAsync<T>(this DbConnection connection, string columnName, string searchValue)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    await connection.OpenAsync();
                    Type type = typeof(T);
                    command.CommandText = $"SELECT * FROM [{GetTableName(type)}] WHERE {columnName} LIKE @searchValue";
                    var searchParameter = CreateParameter(command, "searchValue", $"%{searchValue}%");
                    command.Parameters.Add(searchParameter);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return ReadResultSet<T>(type, reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static T GetById<T>(this IDbConnection connection, object id)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.OpenConnection();
                    Type type = typeof(T);
                    string tableName = GetTableName(type);
                    string primaryKeyColumnName = GetPrimaryKeyColumnName(type);
                    command.CommandText = $"SELECT * FROM [{tableName}] WHERE {primaryKeyColumnName} = @Id";
                    var idParameter = CreateParameter(command, "Id", id);
                    command.Parameters.Add(idParameter);
                    using (var reader = command.ExecuteReader())
                    {
                        return ReadResultSet<T>(type, reader).FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.CloseConnection();
                }
            }
        }
        public async static Task<T> GetByIdAsync<T>(this DbConnection connection, object id)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    await connection.OpenAsync();
                    Type type = typeof(T);
                    string tableName = GetTableName(type);
                    string primaryKeyColumnName = GetPrimaryKeyColumnName(type);
                    command.CommandText = $"SELECT * FROM [{tableName}] WHERE {primaryKeyColumnName} = @Id";
                    var idParameter = CreateParameter(command, "Id", id);
                    command.Parameters.Add(idParameter);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return ReadResultSet<T>(type, reader).FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public static void DeleteById<T>(this IDbConnection connection, object id)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.OpenConnection();
                    Type type = typeof(T);
                    string primaryKeyColumnName = GetPrimaryKeyColumnName(type);
                    command.CommandText = $"DELETE FROM [{GetTableName(type)}] WHERE {primaryKeyColumnName} = @Id";
                    IDbDataParameter idParameter = command.CreateParameter();
                    idParameter.ParameterName = "@Id";
                    idParameter.Value = id;
                    command.Parameters.Add(idParameter);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.CloseConnection();
                }
            }
        }
        public async static Task DeleteByIdAsync<T>(this DbConnection connection, object id)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    await connection.OpenAsync();
                    Type type = typeof(T);
                    string primaryKeyColumnName = GetPrimaryKeyColumnName(type);
                    command.CommandText = $"DELETE FROM [{GetTableName(type)}] WHERE {primaryKeyColumnName} = @Id";
                    IDbDataParameter idParameter = command.CreateParameter();
                    idParameter.ParameterName = "@Id";
                    idParameter.Value = id;
                    command.Parameters.Add(idParameter);
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }

        }
        public static void UpdateById<T>(this IDbConnection connection, object id, object updatedData)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.OpenConnection();
                    Type type = typeof(T);
                    string tableName = GetTableName(type);
                    string primaryKeyColumnName = GetPrimaryKeyColumnName(type);
                    var updateQuery = CreateUpdateQuery(updatedData, type, primaryKeyColumnName, command);
                    var idParameter = CreateParameter(command, "Id", id);
                    command.Parameters.Add(idParameter);
                    command.CommandText = updateQuery;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.CloseConnection();
                }
            }
        }
        public async static Task<bool> UpdateByIdAsync<T>(this DbConnection connection, object id, object updatedData)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    await connection.OpenAsync();
                    Type type = typeof(T);
                    string tableName = GetTableName(type);
                    string primaryKeyColumnName = GetPrimaryKeyColumnName(type);
                    var updateQuery = CreateUpdateQuery(updatedData, type, primaryKeyColumnName, command);
                    var idParameter = CreateParameter(command, "Id", id);
                    command.Parameters.Add(idParameter);
                    command.CommandText = updateQuery;
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public static IEnumerable<T> ExecuteQuery<T>(this IDbConnection connection, string sqlQuery, object parameters = null) where T : class
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.OpenConnection();
                    command.CommandText = sqlQuery;
                    AddParametersToCommand(command, parameters);
                    using (var reader = command.ExecuteReader())
                    {
                        return ReadResultSet<T>(typeof(T), reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.CloseConnection();
                }
            }
        }
        public async static Task<IEnumerable<T>> ExecuteQueryAsync<T>(this DbConnection connection, string sqlQuery, object parameters = null) where T : class
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    await connection.OpenAsync();
                    command.CommandText = sqlQuery;
                    AddParametersToCommand(command, parameters);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return ReadResultSet<T>(typeof(T), reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public async static Task<T> ExecuteScalarAsync<T>(this DbConnection connection, string sqlQuery, object parameters = null)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    await connection.OpenAsync();
                    command.CommandText = sqlQuery;
                    AddParametersToCommand(command, parameters);
                    var result = await command.ExecuteScalarAsync();
                    return (T)Convert.ChangeType(result, typeof(T));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public static IEnumerable<T> SelectWhere<T>(this IDbConnection connection, string whereClause, object parameters = null)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.OpenConnection();
                    Type type = typeof(T);
                    string selectQuery = new StringBuilder($"SELECT * FROM [{GetTableName(type)}] WHERE {whereClause}").ToString();
                    AddParametersToCommand(command, parameters);
                    command.CommandText = selectQuery;
                    using (var reader = command.ExecuteReader())
                    {
                        return ReadResultSet<T>(type, reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.CloseConnection();
                }
            }
        }
        public async static Task<IEnumerable<T>> SelectWhereAsync<T>(this DbConnection connection, string whereClause, object parameters = null)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    await connection.OpenAsync();
                    Type type = typeof(T);
                    string selectQuery = new StringBuilder($"SELECT * FROM [{GetTableName(type)}] WHERE {whereClause}").ToString();
                    AddParametersToCommand(command, parameters);
                    command.CommandText = selectQuery;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return ReadResultSet<T>(type, reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public static bool RowExists<T>(this IDbConnection connection, string whereClause, object parameters = null)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.OpenConnection();
                    Type type = typeof(T);
                    string selectQuery = new StringBuilder($"SELECT COUNT(*) FROM [{GetTableName(type)}] WHERE {whereClause}").ToString();
                    AddParametersToCommand(command, parameters);
                    command.CommandText = selectQuery;
                    int rowCount = (int)command.ExecuteScalar();
                    return rowCount > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.CloseConnection();
                }
            }
        }
        public async static Task<bool> RowExistsAsync<T>(this DbConnection connection, string whereClause, object parameters = null)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    await connection.OpenAsync();
                    Type type = typeof(T);
                    string selectQuery = new StringBuilder($"SELECT COUNT(*) FROM [{GetTableName(type)}] WHERE {whereClause}").ToString();
                    AddParametersToCommand(command, parameters);
                    command.CommandText = selectQuery;
                    int rowCount = (int)await command.ExecuteScalarAsync().ConfigureAwait(false);
                    return rowCount > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public static async Task<bool> ExecuteStoredProcedureAsync(this DbConnection connection, string storedProcedureName, object parameters = null)
        {
            await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = storedProcedureName;
                        command.Transaction = transaction;
                        AddParametersToCommand(command, parameters);
                        await command.ExecuteNonQueryAsync();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception: {ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        private static void OpenConnection(this IDbConnection connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }
        private static void CloseConnection(this IDbConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
        private static string CreateInsertQuery(Type type, object parameters, IDbCommand command)
        {
            var insertQuery = new StringBuilder($"INSERT INTO [{GetTableName(type)}] (");
            var valuesQuery = new StringBuilder("VALUES (");
            foreach (var property in type.GetProperties())
            {
                if (property.Name == "Id") continue;
                string columnName = GetColumnName(property);
                IDbDataParameter parameter = CreateParameter(command, columnName, property.GetValue(parameters));
                command.Parameters.Add(parameter);
                insertQuery.Append($"{columnName}, ");
                valuesQuery.Append($"@{columnName}, ");
            }
            insertQuery.Remove(insertQuery.Length - 2, 2).Append(")");
            valuesQuery.Remove(valuesQuery.Length - 2, 2).Append(")");
            insertQuery.Append(" OUTPUT INSERTED.*");
            return $"{insertQuery} {valuesQuery}";
        }
        private static string GetTableName(Type type)
        {
            var tableAttribute = (TableAttribute)Attribute.GetCustomAttribute(type, typeof(TableAttribute));
            return tableAttribute != null ? tableAttribute.Name : type.Name;
        }
        private static string GetColumnName(PropertyInfo property)
        {
            var columnAttribute = (ColumnAttribute)Attribute.GetCustomAttribute(property, typeof(ColumnAttribute));
            return columnAttribute != null ? columnAttribute.Name : property.Name;
        }
        private static IDbDataParameter CreateParameter(IDbCommand command, string parameterName, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = $"@{parameterName}";
            if(value==null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = value;
            }
            return parameter;
        }
        private static IEnumerable<T> ReadResultSet<T>(Type type, IDataReader reader)
        {
            var resultList = new List<T>();
            while (reader.Read())
            {
                var item = CreateInstance<T>(type, reader);
                resultList.Add(item);
            }
            reader.Close();
            return resultList;
        }
        private static T CreateInstance<T>(Type type, IDataReader reader)
        {
            var instance = Activator.CreateInstance<T>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var columnName = reader.GetName(i);
                var property = GetPropertyByName(type, columnName);
                if (property != null)
                {
                    var value = reader.GetValue(i);
                    if (value == DBNull.Value)
                    {
                        if (IsPropertyTypeNullable(property))
                        {
                            property.SetValue(instance, null);
                        }
                    }
                    else
                    {
                        Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        object convertedValue = Convert.ChangeType(value, propertyType);
                        property.SetValue(instance, convertedValue);
                    }
                }
            }
            return instance;
        }
        private static PropertyInfo GetPropertyByName(Type type, string propertyName)
        {
            return type.GetProperties()
                       .FirstOrDefault(property => GetColumnName(property) == propertyName || property.Name == propertyName);
        }
        private static IEnumerable<IDbDataParameter> ToParameters(this object parameters, IDbCommand command)
        {
            return parameters.GetType()
                .GetProperties()
                .Select(property =>
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = $"@{property.Name}";
                    var value = property.GetValue(parameters);
                    parameter.Value = value ?? DBNull.Value;
                    return parameter;
                });
        }
        private static string GetPrimaryKeyColumnName(Type type)
        {
            string primaryKeyColumnName = "Id";
            var primaryKeyProperty = type.GetProperty(primaryKeyColumnName);
            if (primaryKeyProperty != null)
            {
                var primaryKeyColumnAttribute = (ColumnAttribute)Attribute.GetCustomAttribute(primaryKeyProperty, typeof(ColumnAttribute));
                primaryKeyColumnName = primaryKeyColumnAttribute != null ? primaryKeyColumnAttribute.Name : primaryKeyColumnName;
            }
            return primaryKeyColumnName;
        }
        private static string CreateUpdateQuery(object updatedData, Type type, string primaryKeyColumnName, IDbCommand command)
        {
            var updateQuery = new StringBuilder($"UPDATE [{GetTableName(type)}] SET ");
            foreach (var property in updatedData.GetType().GetProperties())
            {
                if (property.Name != "Id")
                {
                    var columnName = GetColumnName(property);
                    var parameter = CreateParameter(command, property.Name, property.GetValue(updatedData));
                    command.Parameters.Add(parameter);
                    updateQuery.Append($"{columnName} = @{property.Name}, ");
                }
            }
            updateQuery.Remove(updateQuery.Length - 2, 2);
            updateQuery.Append($" WHERE {primaryKeyColumnName} = @Id");
            return updateQuery.ToString();
        }
        private static void AddParametersToCommand(IDbCommand command, object parameters)
        {
            if (parameters != null)
            {
                foreach (var parameter in parameters.ToParameters(command))
                {
                    command.Parameters.Add(parameter);
                }
            }
        }
        private static bool IsPropertyTypeNullable(PropertyInfo property)
        {
            return !property.PropertyType.IsValueType || Nullable.GetUnderlyingType(property.PropertyType) != null;
        }
    }
}
