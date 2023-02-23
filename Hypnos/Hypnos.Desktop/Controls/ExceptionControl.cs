using System;
using System.Windows.Forms;

namespace Hypnos.Desktop.Controls
{
    public partial class ExceptionControl : UserControl
    {
        private Exception exception;

        public Exception Exception
        {
            get => exception;
            set
            {
                exception = value;
                messageBox.Text = exception.Message;
                stackBox.Text = exception.StackTrace;
            }
        }

        public ExceptionControl()
        {
            InitializeComponent();
        }
    }
}
