using Hypnos.Desktop.Repositories;
using Hypnos.Desktop.Utils;
using System.Windows.Forms;

namespace Hypnos.Desktop.Forms
{
    public partial class AuthenticationForm : Form
    {
        public AuthenticationForm()
        {
            InitializeComponent();
            authenticationTabs.TabPages.Remove(passwordTab);
        }

        /// <summary>
        /// If a user cancels the authentication, the application should exit.
        /// </summary>
        private void Exit(object sender, LinkLabelLinkClickedEventArgs e) => Program.Exit();

        private void Exit(object sender, FormClosedEventArgs e)
        {
            if (!AuthenticationUtility.IsAuthenticated)
            {
                Program.Exit();
            }
        }

        /// <summary>
        /// Enables the Sign In link if the login text box value is present and disables the link otherwise.
        /// </summary>
        private void ValidateLogin(object sender, System.EventArgs e)
        {
            checkLoginLink.Text = (checkLoginLink.Enabled = ((TextBoxBase)sender).Text.Length > 0)
                ? "Войти"
                : "Введите логин";
        }

        /// <summary>
        /// Returns the user back to login tab.
        /// </summary>
        private void ReturnBack(object sender, LinkLabelLinkClickedEventArgs e)
        {
            authenticationTabs.TabPages.Add(loginTab);
            authenticationTabs.TabPages.Remove(passwordTab);
            AcceptButton = checkLoginLink;
        }

        /// <summary>
        /// Sends the login name to the database to check if it exists.
        /// </summary>
        private void CheckLogin(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bool loginExists;

            using (var repository = new AuthRepository())
            {
                loginExists = repository.CheckLogin(loginBox.Text);
            }

            if (checkLoginLink.Enabled = loginExists)
            {
                authenticationTabs.TabPages.Add(passwordTab);
                authenticationTabs.TabPages.Remove(loginTab);
                AcceptButton = checkPasswordLink;
            }
            else
            {
                checkLoginLink.Text = "Логин не найден";
            }
        }

        /// <summary>
        /// Enables the link to send a request for a password hash comparison.
        /// </summary>
        private void ValidatePassword(object sender, System.EventArgs e)
        {
            checkPasswordLink.Text = (checkPasswordLink.Enabled = ((TextBoxBase)sender).Text.Length > 0)
                ? "Войти"
                : "Введите пароль";
        }

        private void CheckPassword(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string token = string.Empty;

            using (var repository = new AuthRepository())
            {
                token = repository.Authenticate(loginBox.Text, HashUtility.HashPassword(passwordBox.Text));
            }

            if (checkPasswordLink.Enabled = !string.IsNullOrWhiteSpace(token))
            {
                AuthenticationUtility.Token = token;
                this.Close();

            }
            else
            {
                checkPasswordLink.Text = "Пароль не верен";
            }
        }
    }
}
