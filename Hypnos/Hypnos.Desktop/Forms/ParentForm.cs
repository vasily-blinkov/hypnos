using Hypnos.Desktop.Repositories;
using Hypnos.Desktop.Utils;
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
            if (ExceptionsUtility.IsTerminating)
            {
                return; // because called from another form
            }

            Program.Exit();
        }
    }
}
