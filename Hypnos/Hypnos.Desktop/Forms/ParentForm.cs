using Hypnos.Desktop.Repositories;
using System.Windows.Forms;

namespace Hypnos.Desktop.Forms
{
    public partial class ParentForm : Form
    {
        public ParentForm()
        {
            InitializeComponent();
        }

        private void Ready(object sender, System.EventArgs e)
        {
            CleanupSessions();
        }

        private void Exit(object sender, FormClosedEventArgs e)
        {
            CleanupSessions();
            Program.Exit();
        }

        private void CleanupSessions()
        {
            using (var repository = new AuthRepository())
            {
                repository.CleanupSessions();
            }
        }
    }
}
