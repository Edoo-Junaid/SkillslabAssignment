using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SkillslabAssigment.DAL.Common
{
    public static class IDConnectionExtensions
    {

        public static void ExecuteNonQuery(this IDbConnection connection, string query, object parameters = null)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.OpenConnection();
                    command.CommandText = query;

                    if (parameters != null)
                    {
                        foreach (var property in parameters.GetType().GetProperties())
                        {
                            IDbDataParameter parameter = command.CreateParameter();
                            parameter.ParameterName = $"@{property.Name}";
                            parameter.Value = property.GetValue(parameters);
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
        }
        public static void ExecuteTransaction(this IDbConnection connection, string transactionQuery, object parameters = null)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            command.CommandText = transactionQuery;
                            command.Transaction = transaction;
                            if (parameters != null)
                            {
                                foreach (var property in parameters.GetType().GetProperties())
                                {
                                    IDbDataParameter parameter = command.CreateParameter();
                                    parameter.ParameterName = $"@{property.Name}";
                                    parameter.Value = property.GetValue(parameters);
                                    command.Parameters.Add(parameter);
                                }
                            }
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Exception: {ex.Message}");
                            transaction.Rollback();
                        }
                    }
                }
                finally
                {
                    connection.Close(); // Make sure to close the connection after the transaction
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
                    string tableName = GetTableName(type);
                    var insertQuery = CreateInsertQuery(type, parameters, command);
                    command.CommandText = insertQuery;
                    //command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        return ReadSingleResult<T>(type, reader);
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    return default(T);
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
                    string tableName = GetTableName(type);
                    command.CommandText = $"SELECT * FROM [{tableName}]";
                    using (var reader = command.ExecuteReader())
                    {
                        return ReadResultSet<T>(type, reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    return null;
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
                    command.CommandText = $"SELECT * FROM {tableName} WHERE {primaryKeyColumnName} = @Id";
                    var idParameter = CreateParameter(command, "Id", id);
                    command.Parameters.Add(idParameter);
                    using (var reader = command.ExecuteReader())
                    {
                        return ReadSingleResult<T>(type, reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    return default(T);
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
                    string tableName = GetTableName(type);
                    string primaryKeyColumnName = GetPrimaryKeyColumnName(type);
                    command.CommandText = $"DELETE FROM {tableName} WHERE {primaryKeyColumnName} = @Id";
                    var idParameter = command.CreateParameter();
                    idParameter.ParameterName = "@Id";
                    idParameter.Value = id;
                    command.Parameters.Add(idParameter);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
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
                    Console.WriteLine($"Executing command: {command.CommandText}");
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
                finally
                {
                    connection.CloseConnection();
                }
            }
        }
        public static IEnumerable<T> ExecuteQuery<T>(this IDbConnection connection, string sqlQuery, object parameters = null)
        {
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.OpenConnection();
                    command.CommandText = sqlQuery;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters.ToParameters(command))
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    using (var reader = command.ExecuteReader())
                    {
                        return ReadResultSet<T>(typeof(T), reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    return null;
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
                    var selectQuery = new StringBuilder($"SELECT * FROM [{type.Name}] WHERE {whereClause}");

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters.ToParameters(command))
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.CommandText = selectQuery.ToString();
                    using (var reader = command.ExecuteReader())
                    {
                        return ReadResultSet<T>(type, reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    return null;
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
            var item = Activator.CreateInstance<T>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var columnName = reader.GetName(i);
                var property = GetPropertyByName(type, columnName);
                if (property != null)
                {
                    var value = reader.GetValue(i);

                    if (value != DBNull.Value)
                    {
                        // If the property type is nullable, use the underlying type for comparison
                        Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                        // Convert the value to the property type
                        object convertedValue = Convert.ChangeType(value, propertyType);

                        // Set the property value
                        property.SetValue(item, convertedValue);
                    }
                    else
                    {
                        // Handle DBNull for non-nullable value types 
                        if (!property.PropertyType.IsValueType || Nullable.GetUnderlyingType(property.PropertyType) != null)
                        {
                            // Set the property to its default value
                            property.SetValue(item, null);
                        }
                    }
                }
            }
            return item;
        }
        private static PropertyInfo GetPropertyByName(Type type, string propertyName)
        {
            return type.GetProperties()
                .FirstOrDefault(p =>
                    (Attribute.GetCustomAttribute(p, typeof(ColumnAttribute)) as ColumnAttribute)?.Name == propertyName
                    || p.Name == propertyName);
        }
        private static IDbDataParameter[] ToParameters(this object parameters, IDbCommand command)
        {
            return parameters.GetType()
                .GetProperties()
                .Select(property =>
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = $"@{property.Name}";
                    parameter.Value = property.GetValue(parameters);
                    return parameter;
                })
                .ToArray();
        }
        private static T ReadSingleResult<T>(Type type, IDataReader reader)
        {
            if (reader.Read())
            {
                return CreateInstance<T>(type, reader);
            }
            else
            {
                return default(T);
            }
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
            var updateQuery = new StringBuilder($"UPDATE {GetTableName(type)} SET ");

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

    }

}
