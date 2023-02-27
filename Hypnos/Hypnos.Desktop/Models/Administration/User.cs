using System.ComponentModel;

namespace Hypnos.Desktop.Models.Administration
{
    public class User : Entity
    {
        [DisplayName("ФИО")]
        public string FullName { get; set; }

        [DisplayName("Логин")]
        public string LoginName { get; set; }
    }
}
