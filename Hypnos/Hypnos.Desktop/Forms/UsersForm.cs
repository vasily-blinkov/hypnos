using System;
using System.Windows.Forms;

namespace Hypnos.Desktop.Forms
{
    public partial class UsersForm : Form
    {
        public UsersForm()
        {
            InitializeComponent();
        }

        private void Transpose(object sender, EventArgs e)
        {
            splitContainer.Orientation = splitContainer.Orientation == Orientation.Vertical
                ? Orientation.Horizontal
                : Orientation.Vertical;
        }
    }
}
