using System.ComponentModel;

namespace Hypnos.Desktop.Models.Administration.User
{
    public class UserForDetail : UserForGrid
    {
        [DisplayName("Описание")]
        public string Description { get; set; }
    }
}
