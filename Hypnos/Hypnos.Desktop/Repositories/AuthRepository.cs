using System.Data;

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
            return Execute<bool>("Auth.DoesLoginExist", returns: SqlDbType.Bit, new[]
            {
                new Parameter { Name = "@login_name", Value = loginName }
            });
        }
    }
}
