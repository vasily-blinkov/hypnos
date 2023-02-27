using System;
using System.Windows.Forms;
using Hypnos.Desktop.Models.Administration.User;
using Hypnos.Desktop.Repositories;
using Hypnos.Desktop.Utils;

namespace Hypnos.Desktop.Forms
{
    public partial class UsersForm : Form
    {
        private short userID;

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

        private void LoadUser(object sender, DataGridViewCellEventArgs e)
        {
            var idValue = usersGrid.Rows[e.RowIndex].Cells[nameof(UserForGrid.ID)].Value;

            if (!short.TryParse(idValue != null ? (string)idValue : string.Empty, out var userID) || userID == this.userID)
            {
                return;
            }

            this.userID = userID;
            UserForDetail user;

            using (var repository = new AdministrationRepository())
            {
                user = repository.GetSingleUser(userID);
            }

            fullNameBox.Text = user.FullName;
            loginBox.Text = user.LoginName;
            descriptionBox.Text = user.Description;
        }
    }
}
