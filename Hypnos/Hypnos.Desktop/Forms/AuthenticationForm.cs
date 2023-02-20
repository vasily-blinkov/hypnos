using Hypnos.Desktop.Repositories;
using System.Configuration;
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
        private void Exit(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
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
            }
            else
            {
                checkLoginLink.Text = "Логин не найден";
            }
        }

        /// <summary>
        /// Returns the user back to login tab.
        /// </summary>
        private void ReturnBack(object sender, LinkLabelLinkClickedEventArgs e)
        {
            authenticationTabs.TabPages.Add(loginTab);
            authenticationTabs.TabPages.Remove(passwordTab);
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
        /// Enables the link to send a request for a password hash comparison.
        /// </summary>
        private void ValidatePassword(object sender, System.EventArgs e)
        {
            checkPasswordLink.Text = (checkPasswordLink.Enabled = ((TextBoxBase)sender).Text.Length > 0)
                ? "Войти"
                : "Введите пароль";
        }
    }
}
