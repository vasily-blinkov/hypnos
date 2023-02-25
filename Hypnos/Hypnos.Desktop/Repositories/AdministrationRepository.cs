using System.Collections.Generic;
using System.Data.SqlClient;
using Hypnos.Desktop.Models.Administration;
using Hypnos.Desktop.Utils;

namespace Hypnos.Desktop.Repositories
{
    public class AdministrationRepository : RepositoryBase
    {
        protected override string SchemaName => "Administration";

        /// <summary>
        /// Get roles.
        /// </summary>
        /// <param name="userId">If this is omitted, the stored procedure will return all the roles.</param>
        public List<Role> GetRoles(short userId)
        {
            return ExecuteReader<Role>("GetRoles",
                role => new Role
                {
                    ID = (short)role[nameof(Role.ID)],
                    Name = (string)role[nameof(Role.Name)],
                    Description = (string)role[nameof(Role.Description)]
                },
                new SqlParameter { ParameterName = "@user_id", Value = userId },
                new SqlParameter { ParameterName = "@token", Value = AuthenticationUtility.Token } // TODO: Move adding a token to the base class, method ExecuteAuthenticated.
            );
        }
    }
}
