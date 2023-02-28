﻿using System;
using System.Windows.Forms;
using Hypnos.Desktop.Models.Administration.User;
using Hypnos.Desktop.Repositories;
using Hypnos.Desktop.Utils;

namespace Hypnos.Desktop.Forms
{
    public partial class UsersForm : Form
    {
        private short? userID;

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
            LoadUser();
        }

        private void FillGrid()
        {
            using (var repository = new AdministrationRepository())
            {
                usersGrid.DataSource = repository.GetUsers();
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
    }
}
