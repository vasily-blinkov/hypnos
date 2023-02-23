using System;
using System.Windows.Forms;
using Hypnos.Desktop.Forms;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            new AuthenticationForm().ShowDialog();
            Application.Run(ParentForm);
        }

        private static ParentForm CreateParentForm()
        {
            var parentForm = new ParentForm();
            parentForm.Visible = false;
            return parentForm;
        }

        public static void Exit() => Application.Exit();
    }
}
