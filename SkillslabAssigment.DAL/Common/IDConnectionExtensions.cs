using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
                    connection.OpenConnection();
                    command.CommandText = query;
                    AddParametersToCommand(command, parameters);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
            }
        }
        //public static async Task ExecuteNonQueryAsync(this DbConnection connection, string query, object parameters = null)
        //{
        //    using (var command = connection.CreateCommand())
        //    {
        //        try
        //        {
        //            connection.OpenConnection();
        //            command.CommandText = query;
        //            AddParametersToCommand(command, parameters);
        //            command.ExecuteNonQuery();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Exception: {ex.Message}");
        //            throw;
        //        }
        //    }
        //}
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
            parameter.Value = value;
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
