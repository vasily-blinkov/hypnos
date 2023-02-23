using System.Windows.Forms;

namespace Hypnos.Desktop.Forms
{
    public partial class ParentForm : Form
    {
        public ParentForm()
        {
            InitializeComponent();
        }

        private void Exit(object sender, FormClosedEventArgs e)
        {
            Program.Exit();
        }
    }
}
