using System;
using System.ComponentModel;
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
        private enum Mode { Main, Create };

        private short? userID;
        private BindingList<Role> roles;

        private readonly ToolStripModeUtility<Mode> modeUtility;

        public UsersForm()
        {
            InitializeComponent();
            modeUtility = InitializeModes();
        }

        private ToolStripModeUtility<Mode> InitializeModes()
        {
            return new ToolStripModeUtility<Mode>(toolStrip)
                .Map(Mode.Main, filterLabel, filterBox, readButton, masterDetailsSeparator, crudLabel, createButton);
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

            ChangeUser(userID.Value);
        }

        private void ChangeUser(short userID)
        {
            LoadUser(userID);
            this.userID = userID;
        }

        private void LoadUser(short userID)
        {
            UserForDetail user;

            using (var repository = new AdministrationRepository())
            {
                user = repository.GetSingleUser(userID);
            }

            FillDetail(user);
            FillRoles(userID);
        }

        /// <returns>
        /// ID of a selected user row in the grid view
        /// </returns>
        private short? GetSelectedUserID(int rowIndex)
        {
            if (rowIndex < 0)
            {
                return null; // nothing selected
            }

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
            passwordBox.Clear();
        }

        private void FillRoles(short userID)
        {
            BindingList<Role> roles;

            using (var repository = new AdministrationRepository())
            {
                roles = repository.GetRoles(userID);
            }

            rolesBoxes.UncheckAll();

            for (int index = 0; index < this.roles.Count; index++)
            {
                if (roles.Any(r => r.ID == this.roles[index].ID))
                {
                    rolesBoxes.SetItemChecked(index, true);
                }
            }
        }

        private void Refresh(object sender, EventArgs e) => Reload();

        private void Reload()
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

        private void CreateUser(object sender, EventArgs e)
        {
            // Add a new row to the table.
            ((BindingList<UserForGrid>)usersGrid.DataSource).Add(new UserForGrid());

            // Select the created row.
            usersGrid.ClearSelection();
            usersGrid.Rows[usersGrid.RowCount - 1].Selected = true;

            // Clear details.
            FillDetail(new UserForDetail());

            // Change visible tool strip items.
            modeUtility.Switch(Mode.Create);
        }

        private void CancelCreateUser()
        {
            modeUtility.Mode = Mode.Main;
            ((BindingList<UserForGrid>)usersGrid.DataSource).RemoveAt(usersGrid.RowCount - 1);
        }

        private void DeleteUser(object sender, EventArgs e)
        {
            if (modeUtility.Mode == Mode.Create && ConfirmCancelCreateUser() == DialogResult.Yes)
            {
                CancelCreateUser();

                // Select previously selected row.
                if (this.userID.HasValue)
                {
                    SelectRow(userID.Value);
                    LoadUser(userID.Value);
                }
            }
        }

        private void SelectUser(object sender, EventArgs e)
        {
            if (modeUtility.Mode == Mode.Create && !usersGrid.Rows[usersGrid.RowCount - 1].Selected)
            {
                if (ConfirmCancelCreateUser() == DialogResult.Yes) 
                {
                    CancelCreateUser();
                    LoadUser(usersGrid.SelectedRows[0].Index);
                }
                else
                {
                    usersGrid.Rows[usersGrid.RowCount - 1].Selected = true;
                }
            }
        }

        private DialogResult ConfirmCancelCreateUser() => MessageBox.Show(
            "Форма содержит данные, которые пока не были сохранены в базу. При продолжении без сохранения они будут безвозвратно утеряны.",
            "Отменить создание пользователя?",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        private void HandleClosing(object sender, FormClosingEventArgs e)
        {
            if (modeUtility.Mode == Mode.Create && ConfirmCancelCreateUser() != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void SaveUser(object sender, EventArgs e)
        {
            if (Mode.Create == modeUtility.Mode)
            {
                AddUser();
            }
            else
            {
                EditUser();
            }
        }

        private void AddUser()
        {
            using (var repository = new AdministrationRepository())
            {
                repository.AddUser(ReadDetail().ToJsonString());
            }

            modeUtility.Switch(Mode.Main);
            Reload();
        }

        private void EditUser()
        {
            throw new NotImplementedException();
        }

        private UserForUpsert ReadDetail() => new UserForUpsert
        {
            FullName = fullNameBox.Text,
            LoginName = loginBox.Text,
            Description = descriptionBox.Text,
            PasswordHash = HashUtility.HashPassword(passwordBox.Text)
        };
    }
}
