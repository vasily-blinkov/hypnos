using System;
using System.Windows.Forms;
using Hypnos.Desktop.Controls;
using Hypnos.Desktop.Utils;

namespace Hypnos.Desktop.Forms
{
    public partial class ExceptionForm : Form
    {
        private Exception exception;

        public Exception Exception
        {
            get => exception;
            set
            {
                exception = value;
                RemoveExceptionPages();
                AddExceptionPages(exception);
            }
        }

        public ExceptionForm()
        {
            InitializeComponent();
        }

        private void RemoveExceptionPages()
        {
            foreach (TabPage page in detailsTabs.TabPages)
            {
                if (page != warningPage)
                {
                    detailsTabs.TabPages.Remove(page);
                }
            }
        }

        private void AddExceptionPages(Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            var exceptionControl = new ExceptionControl();
            exceptionControl.Exception = exception;
            exceptionControl.Dock = DockStyle.Fill;

            var page = new TabPage(exception.GetType().Name);
            page.Controls.Add(exceptionControl);

            detailsTabs.TabPages.Add(page);
            AddExceptionPages(exception.InnerException);
        }

        public void ShowDialog(bool isTerminating)
        {
            ExceptionsUtility.IsTerminating = isTerminating;
            continueLink.Visible = !ExceptionsUtility.IsTerminating;
            ((Form)this).ShowDialog();
        }

        public new void ShowDialog()
        {
            ShowDialog(isTerminating: false);
        }

        public new void Show() => ReportUnsupportedMethod();

        public new void Show(IWin32Window win) => ReportUnsupportedMethod();

        public new void ShowDialog(IWin32Window win) => ReportUnsupportedMethod();

        private void ReportUnsupportedMethod()
            => throw new NotSupportedException("The method called is not supported. Please, use explicitly defines ones.");

        private void Terminate(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();

            if (!ExceptionsUtility.IsTerminating)
            {
                // Set the flag to make the ParentForm exit safely to avoid further possible damage.
                ExceptionsUtility.IsTerminating = true;

                // Just closes the exception window when it opened when handling terminating exception.
                // Therefore execution will continue to be terminated by CLR.
                Program.ExitSafe();
            }
        }

        private void ContinueProgram(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }
    }
}
