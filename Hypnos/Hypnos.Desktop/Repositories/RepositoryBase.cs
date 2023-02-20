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

        protected T Execute<T>(string procedureName, SqlDbType returns, IEnumerable<Parameter> parameters = null)
        {
            using (var command = new SqlCommand(procedureName, connection) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add("@result", returns).Direction = ParameterDirection.ReturnValue;

                if ((parameters?.Any()).GetValueOrDefault())
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Name, parameter.Value);
                    }
                }

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                command.ExecuteScalar();

                return (T)command.Parameters["@result"].Value;
            }
        }
    }
}
