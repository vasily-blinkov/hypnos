using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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

        protected SqlParameterCollection Execute(string procedureName, params SqlParameter[] parameters)
        {
            var procedurePath = $"{(!string.IsNullOrWhiteSpace(SchemaName) ? $"{SchemaName}." : string.Empty)}{procedureName}";

            using (var command = new SqlCommand(procedurePath, connection) { CommandType = CommandType.StoredProcedure })
            {
                if ((parameters?.Any()).GetValueOrDefault())
                {
                    command.Parameters.AddRange(parameters);
                }

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                command.ExecuteScalar();
                return command.Parameters;
            }
        }
    }
}
