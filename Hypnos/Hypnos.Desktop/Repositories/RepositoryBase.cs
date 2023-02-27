using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Hypnos.Desktop.Utils;

namespace Hypnos.Desktop.Repositories
{
    public abstract class RepositoryBase : IDisposable
    {
        private SqlConnection connection;

        protected virtual string SchemaName => string.Empty;

        public RepositoryBase()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Hypnos"].ConnectionString);
        }

        public void Dispose()
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }

            connection.Dispose();
        }

        protected SqlParameterCollection ExecuteScalar(string procedureName, params SqlParameter[] parameters)
        {
            using (var command = CreateCommand(procedureName, parameters))
            {
                try
                {
                    command.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    ExceptionsUtility.Handle(ex);
                }

                return command.Parameters;
            }
        }

        /// <summary>
        /// Executes reader authenticated.
        /// </summary>
        /// <remarks>
        /// Adds the @token parameter to the end of the <paramref name="parameters"/>.
        /// </remarks>
        protected List<T> ExecuteReaderAuth<T>(
            string procedureName,
            Func<SqlDataReader, T> convert,
            params SqlParameter[] parameters)
        {
            var length = (parameters?.Length).GetValueOrDefault();
            var extendedParameters = new SqlParameter[length + 1];

            Array.Copy(parameters, extendedParameters, length);
            extendedParameters[extendedParameters.Length - 1] = new SqlParameter { ParameterName = "@token", Value = AuthenticationUtility.Token };

            return ExecuteReader(procedureName, convert, extendedParameters);
        }

        protected List<T> ExecuteReader<T>(
            string procedureName,
            Func<SqlDataReader, T> convert,
            params SqlParameter[] parameters)
        {
            List<T> resultCollection = null;

            using (var command = CreateCommand(procedureName, parameters))
            {
                using (var reader = command.ExecuteReader())
                {
                    resultCollection = new List<T>();

                    while (reader.Read())
                    {
                        resultCollection.Add(convert(reader));
                    }
                }
            }

            return resultCollection;
        }

        private SqlCommand CreateCommand(string procedureName, params SqlParameter[] parameters)
        {
            var procedurePath = $"{(!string.IsNullOrWhiteSpace(SchemaName) ? $"{SchemaName}." : string.Empty)}{procedureName}";

            var command = new SqlCommand(procedurePath, connection) { CommandType = CommandType.StoredProcedure };
            if ((parameters?.Any()).GetValueOrDefault())
            {
                command.Parameters.AddRange(parameters);
            }

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return command;
        }
    }
}
