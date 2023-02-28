using System.Collections.Generic;
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
        public List<Role> GetRoles(short? userId = null)
        {
            SqlParameter[] parameters = userId.HasValue
                ? new[] { new SqlParameter { ParameterName = "@user_id", Value = userId } }
                : new SqlParameter[0];

            return ExecuteReaderAuth("GetRoles", ConvertRole.ByDefault, parameters);
        }

        public List<UserForGrid> GetUsers() => ExecuteReaderAuth("GetUsers", ConvertUser.ForGrid);

        public UserForDetail GetSingleUser(short userId) => ExecuteReaderAuth("GetSignleUser", ConvertUser.ForDetail,
            new SqlParameter { ParameterName = "@user_id", Value = userId }
        ).Single();
    }
}
