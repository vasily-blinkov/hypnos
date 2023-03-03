using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Hypnos.Desktop.EqualityComparers;
using Hypnos.Desktop.Models.Administration;
using Hypnos.Desktop.Models.Administration.User;
using Hypnos.Desktop.Repositories;
using Hypnos.Desktop.Utils;

namespace Hypnos.Desktop.Forms
{
    public partial class UsersForm : Form
    {
        private short? userID;
        private List<Role> roles;

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
            LoadRoles();
            LoadUser();
        }

        private void FillGrid()
        {
            using (var repository = new AdministrationRepository())
            {
                usersGrid.DataSource = repository.GetUsers(filterBox.Text);
                GridUtility.Setup(usersGrid);
            }
        }

        private void LoadUser()
        {
            var selectedRows = usersGrid.SelectedRows;

            if (selectedRows.Count == 0)
            {
                return;
            }

            LoadUser(selectedRows[0].Index);
        }

        private void LoadUser(object sender, DataGridViewCellEventArgs e)
        {
            LoadUser(e.RowIndex);
        }

        private void LoadUser(int rowIndex)
        {
            var userID = GetSelectedUserID(rowIndex);

            if (!userID.HasValue)
            {
                return;
            }

            LoadUser(userID.Value);
        }

        private void LoadUser(short userID)
        {
            if (userID == this.userID)
            {
                return;
            }

            UserForDetail user;

            using (var repository = new AdministrationRepository())
            {
                user = repository.GetSingleUser(userID);
            }

            FillDetail(user);
            FillRoles(userID);

            this.userID = userID;
        }

        /// <returns>
        /// ID of a selected user row in the grid view
        /// </returns>
        private short? GetSelectedUserID(int rowIndex)
        {
            var idValue = usersGrid.Rows[rowIndex].Cells[nameof(UserForGrid.ID)].Value;

            return short.TryParse(idValue != null ? idValue.ToString() : string.Empty, out var userID)
                ? userID
                : (short?)null;
        }

        private void FillDetail(UserForDetail user)
        {
            fullNameBox.Text = user.FullName;
            loginBox.Text = user.LoginName;
            descriptionBox.Text = user.Description;
        }

        private void FillRoles(short userID)
        {
            List<Role> roles;

            using (var repository = new AdministrationRepository())
            {
                roles = repository.GetRoles(userID);
            }

            rolesBoxes.UncheckAll();

            for (var index = 0; index < roles.Count; index++)
            {
                if (roles.Any(r => r.ID == this.roles[index].ID))
                {
                    rolesBoxes.SetItemChecked(index, true);
                }
            }
        }

        private void Refresh(object sender, EventArgs e)
        {
            FillGrid();

            // Trying to select a row in the grid matching the currently displaying user in the detail panel.
            if (userID.HasValue)
            {
                SelectRow(userID.Value);
            }

            LoadRoles();
            LoadUser();

            if (userID.HasValue)
            {
                FillRoles(userID.Value);
            }
        }

        private void SelectRow(short userID)
        {
            var row = usersGrid.Rows.Find(nameof(UserForGrid.ID), userID, new IdEqualityComparer());

            if (row == null)
            {
                return;
            }

            row.Selected = true;
        }

        private void LoadRoles()
        {
            using (var repository = new AdministrationRepository())
            {
                roles = repository.GetRoles();
            }

            rolesBoxes.Items.Clear();
            rolesBoxes.Items.AddRange(roles.Select(r => r.Name).ToArray());
        }
    }
}
