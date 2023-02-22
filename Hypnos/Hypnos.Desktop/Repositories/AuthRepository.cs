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
            const string token = "@token";

            return (string)Execute("Auth.Authenticate",
                new SqlParameter { ParameterName = "@login_name", Value = loginName },
                new SqlParameter { ParameterName = "@password_hash", Value = passwordHash },
                new SqlParameter { ParameterName = token, Direction = ParameterDirection.Output, Size = 128 }
            )[token].Value;
        }
    }
}
