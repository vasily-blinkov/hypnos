using System.Data.SqlClient;
using Hypnos.Desktop.Models.Administration.User;

namespace Hypnos.Desktop.Converters
{
    public static class ConvertUser
    {
        public static UserForGrid ForGrid(SqlDataReader user) => ForGrid(user, new UserForGrid());

        public static UserForDetail ForDetail(SqlDataReader user)
        {
            var detail = new UserForDetail();
            ForGrid(user, detail);
            detail.Description = (string)user[nameof(UserForDetail.Description)];
            return detail;
        }

        private static T ForGrid<T>(SqlDataReader source, T destination)
            where T : UserForGrid, new()
        {
            if (destination == null)
            {
                destination = new T();
            }

            destination.ID = (short)source[nameof(UserForGrid.ID)];
            destination.FullName = (string)source[nameof(UserForGrid.FullName)];
            destination.LoginName = (string)source[nameof(UserForGrid.LoginName)];

            return destination;
        }
    }
}
