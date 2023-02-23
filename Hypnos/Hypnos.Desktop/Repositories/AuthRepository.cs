using System;
using System.Data;
using System.Data.SqlClient;

namespace Hypnos.Desktop.Repositories
{
    public class AuthRepository : RepositoryBase
    {
        /// <summary>
        /// Check if login exists.
        /// </summary>
        /// <param name="loginName">Login name to check if it exists</param>
        /// <returns><c>True</c> if the <paramref name="loginName"/> exists, <c>False</c> otherwise.</returns>
        public bool CheckLogin(string loginName)
        {
            const string result = "@result";

            return (bool)Execute("Auth.DoesLoginExist",
                new SqlParameter { ParameterName = result, SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.ReturnValue },
                new SqlParameter { ParameterName = "@login_name", Value = loginName }
            )[result].Value;
        }

        public string Authenticate(string loginName, string passwordHash)
        {
            const string tokenName = "@token";

            var tokenValue = Execute("Auth.Authenticate",
                new SqlParameter { ParameterName = "@login_name", Value = loginName },
                new SqlParameter { ParameterName = "@password_hash", Value = passwordHash },
                new SqlParameter { ParameterName = tokenName, Direction = ParameterDirection.Output, Size = 128 }
            )[tokenName].Value;

            return tokenValue == DBNull.Value ? string.Empty : (string)tokenValue;
        }

        public void LogOut(string token)
        {
            Execute("Auth.LogOut",
                new SqlParameter { ParameterName = "@token", Value = token }
            );
        }
    }
}
