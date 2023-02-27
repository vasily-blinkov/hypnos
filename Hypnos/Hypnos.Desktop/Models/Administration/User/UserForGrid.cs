using System.ComponentModel;

namespace Hypnos.Desktop.Models.Administration.User
{
    public class UserForGrid : Entity
    {
        [DisplayName("ФИО")]
        public string FullName { get; set; }

        [DisplayName("Логин")]
        public string LoginName { get; set; }
    }
}
