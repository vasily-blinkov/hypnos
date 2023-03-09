using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using Hypnos.Desktop.Converters;
using Hypnos.Desktop.Models.Administration;
using Hypnos.Desktop.Models.Administration.User;

namespace Hypnos.Desktop.Repositories
{
    public class AdministrationRepository : RepositoryBase
    {
        protected override string SchemaName => "Administration";

        /// <param name="userId">
        /// If this is omitted, the stored procedure will return all the roles.
        /// Otherwise only for the user with the specified ID
        /// </param>
        public BindingList<Role> GetRoles(short? userId = null)
        {
            SqlParameter[] parameters = userId.HasValue
                ? new[] { new SqlParameter { ParameterName = "@user_id", Value = userId } }
                : new SqlParameter[0];

            return ExecuteReaderAuth("GetRoles", ConvertRole.ByDefault, parameters);
        }

        public BindingList<UserForGrid> GetUsers(string query = null)
        {
            SqlParameter[] parameters = !string.IsNullOrWhiteSpace(query)
                ? new[] { new SqlParameter { ParameterName = "@query", Value = query } }
                : new SqlParameter[0];

            return ExecuteReaderAuth("GetUsers", ConvertUser.ForGrid, parameters);
        }

        public UserForDetail GetSingleUser(short userId) => ExecuteReaderAuth("GetSignleUser", ConvertUser.ForDetail,
            new SqlParameter { ParameterName = "@user_id", Value = userId }
        ).Single();

        public int AddUser(string userJson) => ExecuteCommandAuth("AddUser", new[] { new SqlParameter("@user_json", userJson) });

        public int EditUser(string userJson) => ExecuteCommandAuth("EditUser", new[] { new SqlParameter("@user_json", userJson) });
    }
}
