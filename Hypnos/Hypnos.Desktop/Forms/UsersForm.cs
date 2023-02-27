using System;
using System.Data;
using System.Windows.Forms;
using Hypnos.Desktop.Repositories;
using Hypnos.Desktop.Utils;

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

        private void Prepare(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            using (var repository = new AdministrationRepository())
            {
                usersGrid.DataSource = repository.GetUsers();
                GridUtility.Setup(usersGrid);
            }
        }
    }
}
