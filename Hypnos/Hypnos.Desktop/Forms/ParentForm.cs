using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Hypnos.Desktop.Models.Administration;
using Hypnos.Desktop.Repositories;
using Hypnos.Desktop.Utils;

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

        public void ApplyRoles()
        {
            BindingList<Role> rolesCollection;

            using (var repository = new AdministrationRepository())
            {
                rolesCollection = repository.GetRoles(AuthenticationUtility.UserId.Value);
            }

            Action<string, ToolStripMenuItem> apply = (string role, ToolStripMenuItem item) =>
            {
                if (!rolesCollection.Any(r => r.Name == role))
                {
                    item.Visible = false;
                }
            };

            apply("Администратор", administrationItem);
            apply("Руководитель", managementItem);
            apply("Специалист", workloadItem);
        }

        private void OpenUsers(object sender, EventArgs e)
        {
            var form = new UsersForm();
            form.MdiParent = this;
            form.Show();
        }
    }
}
