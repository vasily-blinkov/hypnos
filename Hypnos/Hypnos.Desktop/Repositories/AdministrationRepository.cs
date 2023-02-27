using System.Collections.Generic;
using System.Data.SqlClient;
using Hypnos.Desktop.Models.Administration;

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
            return ExecuteReaderAuth("GetRoles",
                role => new Role
                {
                    ID = (short)role[nameof(Role.ID)],
                    Name = (string)role[nameof(Role.Name)],
                    Description = (string)role[nameof(Role.Description)]
                },
                new SqlParameter { ParameterName = "@user_id", Value = userId }
            );
        }

        public List<User> GetUsers()
        {
            return ExecuteReaderAuth("GetUsers",
                user => new User
                {
                    ID = (short)user[nameof(User.ID)],
                    FullName = (string)user[nameof(User.FullName)],
                    LoginName = (string)user[nameof(User.LoginName)]
                }
            );
        }
    }
}
