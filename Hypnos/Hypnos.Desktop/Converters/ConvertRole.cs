using System.Data.SqlClient;
using Hypnos.Desktop.Models.Administration;

namespace Hypnos.Desktop.Converters
{
    public static class ConvertRole
    {
        public static Role ByDefault(SqlDataReader role) => new Role
        {
            ID = (short)role[nameof(Role.ID)],
            Name = (string)role[nameof(Role.Name)],
            Description = (string)role[nameof(Role.Description)]
        };
    }
}
