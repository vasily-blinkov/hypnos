using System;
using System.Diagnostics;
using System.Windows.Forms;
using Hypnos.Desktop.Forms;
using Hypnos.Desktop.Repositories;
using Hypnos.Desktop.Utils;

namespace Hypnos.Desktop
{
    internal static class Program
    {
        private static ParentForm parentForm;
        public static ParentForm ParentForm => parentForm ?? (parentForm = CreateParentForm());

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Setup();
            Start();
        }

        private static void Setup()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        private static void Start()
        {
            SubscribeExceptionHandler();
            CleanupSessions();
            new AuthenticationForm().ShowDialog();
            Application.Run(ParentForm);
        }

        private static void SubscribeExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;
        }

        private static void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionsUtility.Handle((Exception)e.ExceptionObject, e.IsTerminating);
        }

        private static ParentForm CreateParentForm()
        {
            var parentForm = new ParentForm();
            parentForm.Visible = false;
            return parentForm;
        }

        public static void Exit()
        {
            LogOut();
            CleanupSessions();

            ExitSafe();
        }

        public static void ExitSafe()
        {
            Application.Exit();
        }

        private static void LogOut()
        {
            if (!AuthenticationUtility.IsAuthenticated)
            {
                return;
            }

            using (var repository = new AuthRepository())
            {
                repository.LogOut(AuthenticationUtility.Token);
            }
        }

        private static void CleanupSessions()
        {
            using (var repository = new AuthRepository())
            {
                repository.CleanupSessions();
            }
        }
    }
}
