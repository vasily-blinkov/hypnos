using System;
using System.Data;
using System.Data.SqlClient;
using Hypnos.Desktop.Models.Auth;

namespace Hypnos.Desktop.Repositories
{
    public class AuthRepository : RepositoryBase
    {
        protected override string SchemaName => "Auth";

        /// <summary>
        /// Check if login exists.
        /// </summary>
        /// <param name="loginName">Login name to check if it exists</param>
        /// <returns><c>True</c> if the <paramref name="loginName"/> exists, <c>False</c> otherwise.</returns>
        public bool CheckLogin(string loginName)
        {
            const string result = "@result";

            return (bool)ExecuteScalar("DoesLoginExist",
                new SqlParameter { ParameterName = result, SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.ReturnValue },
                new SqlParameter { ParameterName = "@login_name", Value = loginName }
            )[result].Value;
        }

        public AuthResult Authenticate(string loginName, string passwordHash)
        {
            const string tokenName = "@token";
            const string userIdName = "@user_id";

            var parameterCollection = ExecuteScalar("Authenticate",
                new SqlParameter { ParameterName = "@login_name", Value = loginName },
                new SqlParameter { ParameterName = "@password_hash", Value = passwordHash },
                new SqlParameter { ParameterName = tokenName, Direction = ParameterDirection.Output, Size = 128 },
                new SqlParameter { ParameterName = userIdName, Direction = ParameterDirection.Output, SqlDbType = SqlDbType.SmallInt }
            );

            var tokenValue = parameterCollection[tokenName].Value;
            var userIdValue = parameterCollection[userIdName].Value;

            return tokenValue != DBNull.Value
                ? new AuthResult { Token = (string)tokenValue, UserId = (short)userIdValue }
                : null;
        }

        public void LogOut(string token)
        {
            ExecuteScalar("LogOut",
                new SqlParameter { ParameterName = "@token", Value = token }
            );
        }

        /// <summary>
        /// Removes outdated sessions.
        /// </summary>
        public void CleanupSessions()
        {
            ExecuteScalar("CleanupSessions");
        }
    }
}
