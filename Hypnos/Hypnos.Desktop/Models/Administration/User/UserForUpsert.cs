namespace Hypnos.Desktop.Models.Administration.User
{
    public class UserForUpsert
    {
        public string FullName { get; set; }

        public string LoginName { get; set; }

        public string Description { get; set; }

        public string PasswordHash { get; set; }
    }
}
